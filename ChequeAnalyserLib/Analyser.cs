using ChequeAnalyserLib.Base;
using ChequeAnalyserLib.Interfaces;
using ChequeAnalyserLib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;


namespace ChequeAnalyserLib
{
    public class Analyser
    {
        private List<IChequeTemplate> _chequeTemplates;
        private readonly HttpClient _httpClient;
        private TransformBlock<string, QueuedImage> _imagesBlock;
        private TransformBlock<QueuedImage, AnalysedImage> _resolverBlock;
        private TransformBlock<AnalysedImage, ChequeAnalysis> _localAnalysisBlock;
        public ImageManager _imageManager;


        public Analyser(List<IChequeTemplate> chequeTemplates, ActionBlock<ChequeAnalysis> chequeAnalysis)
        {
            _chequeTemplates = chequeTemplates;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add(Constants.SubscriptionHeaderKeyName, Constants.COMPUTER_VISION_SUBSCRIPTION_KEY);

            _imagesBlock = new TransformBlock<string, QueuedImage>((Func<string, Task<QueuedImage>>)QueueImageAnalysisAsync);
            _resolverBlock = new TransformBlock<QueuedImage, AnalysedImage>((Func<QueuedImage, Task<AnalysedImage>>)ResolveImageAsync);
            _localAnalysisBlock = new TransformBlock<AnalysedImage, ChequeAnalysis>((Func<AnalysedImage, ChequeAnalysis>)RunChequeAnalysis);

            _imagesBlock.LinkTo(_resolverBlock);
            _resolverBlock.LinkTo(_localAnalysisBlock);
            _localAnalysisBlock.LinkTo(chequeAnalysis);

            _imageManager = new ImageManager();
        }

        public bool EnqueueImageAnalysis(string imageFilePath)
        {            
            return _imagesBlock.Post(imageFilePath);
        }

        private async Task<QueuedImage> QueueImageAnalysisAsync(string imageFilePath)
        {
            ByteArrayContent content = new ByteArrayContent(_imageManager.GetImageAsByteArray(imageFilePath));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            var response = await _httpClient.PostAsync(Constants.RequestParameters, content);
            return new QueuedImage()
            {
                FilePath = imageFilePath,
                ResourceURL = response.Headers.GetValues("Operation-Location").FirstOrDefault()
            }; 
        }

        private async Task<AnalysedImage> ResolveImageAsync(QueuedImage queuedImage)
        {
            await Task.Delay(1000); /* delay one before querying as results aren't processed super fast */
            var response = await _httpClient.GetAsync(queuedImage.ResourceURL);
            ImageAnalysis result = JsonConvert.DeserializeObject<ImageAnalysis>(await response.Content.ReadAsStringAsync());
            if (!result.IsSuccessStatus)
            {
                _resolverBlock.Post(queuedImage);
            }

            return new AnalysedImage()
            {
                Result = result,
                QueuedImage = queuedImage
            };
        }

        private ChequeAnalysis RunChequeAnalysis(AnalysedImage image)
        {
            IChequeTemplate chequeTemplate = DetermineChequeTemplate(image);
            image.ChequeTemplate = chequeTemplate;

            ChequeAnalysis chequeAnalysis = new ChequeAnalysis
            (
                accountNumber: GetProperty(chequeTemplate.AccountNumber, image)?.Text,
                amount: GetProperty(chequeTemplate.Amount, image)?.Text,
                date: GetProperty(chequeTemplate.Date, image)?.Text,
                payDescriptionLineOne: GetProperty(chequeTemplate.PayDescriptionLineOne, image)?.Text,
                payDescriptionLineTwo: GetProperty(chequeTemplate.PayDescriptionLineTwo, image)?.Text
            );

            _imageManager.RenderAnalysedChequeBoundingBoxes(image);
            _imageManager.RenderChequeTemplateBoundingBoxes(image);


            return chequeAnalysis;
        }

        #region helper methods

        private IChequeTemplate DetermineChequeTemplate(AnalysedImage image)
        {
            return _chequeTemplates[0]; /* lol */
        }

        private Line GetProperty(AABBBoundingBoxItemWithText boundingBoxWithText, AnalysedImage image)
        {
            foreach(var recognitionResult in image.Result.RecognitionResults)
            {
                foreach(var line in recognitionResult.Lines)
                {
                    if(line.BoundingBox.IsIntersecting(boundingBoxWithText.BoundingBox))
                        return line;
                }
            }
            return null;
        }
    
        #endregion
    }
}
