using System;
using UniRx;
using UnityEngine.Networking;
using Zenject;

namespace Utils
{
    public class ObservableWebRequest : IDisposable
    {
        private readonly UnityWebRequest _request;
        private readonly Subject<DownloadHandler> _requestStream = new Subject<DownloadHandler>();

        private ObservableWebRequest(string url, string[] headers = null)
        {
            _request = new UnityWebRequest(url);

            if (headers != null && headers.Length > 0)
            {
                if (headers.Length % 2 != 0)
                    throw new ArgumentException("[ObservableWebRequest] malformed headers,"
                                                + " should be provided in string pairs");

                for (var i = 0; i < headers.Length; i += 2)
                    _request.SetRequestHeader(headers[i], headers[i + 1]);
            }
        }

        public void Dispose()
        {
            if (_request != null) _request.Dispose();
            _requestStream.Dispose();
        }

        private void SendPostRequest(byte[] data)
        {
            _request.method = UnityWebRequest.kHttpVerbPOST;

            _request.uploadHandler = new UploadHandlerRaw(data);
            _request.downloadHandler = new DownloadHandlerBuffer();

            //Debug.Log(string.Format("[ObservableWebRequest] sending {1} request to {0} with data {2} bytes",
            //                        _request.url,
            //                        _request.method,
            //                        data.Length));

            SendAsObservable();
        }

        private void SendRequest()
        {
            _request.method = UnityWebRequest.kHttpVerbGET;
            _request.downloadHandler = new DownloadHandlerBuffer();

            //Debug.Log(string.Format("[ObservableWebRequest] sending {1} request to {0}",
            //                        _request.url,
            //                        _request.method));

            SendAsObservable();
        }

        private void SendAsObservable()
        {
#pragma warning disable 618
            _request.Send()
#pragma warning restore 618
                .AsObservable()
                .DoOnCompleted(_requestStream.OnCompleted)
                .DoOnError(_requestStream.OnError)
                .Subscribe(operation =>
                {
                    //Debug.Log(string.Format("[ObservableWebRequest] got reply (code: {0}) with text {1}",
                    //                        _request.responseCode,
                    //                        _request.downloadHandler.text));

                    _requestStream.OnNext(_request.downloadHandler);
                });
        }

        public UniRx.IObservable<DownloadHandler> AsObservable()
        {
            return _requestStream;
        }

        public void Abort()
        {
            if (_request == null) return;

            _request.Abort();
            _requestStream.OnError(new OperationCanceledException("[ObservableWebRequest] request aborted"));
        }

        public class Factory
        {
            private readonly IFactory<string, string[], ObservableWebRequest> _webRequestFactory;

            public Factory(IFactory<string, string[], ObservableWebRequest> webRequestFactory)
            {
                _webRequestFactory = webRequestFactory;
            }

            public ObservableWebRequest CreatePostRequest(string url, byte[] postData, params string[] headers)
            {
                var request = _webRequestFactory.Create(url, headers);

                request.SendPostRequest(postData);
                return request;
            }

            public ObservableWebRequest CreateGetRequest(string url, params string[] headers)
            {
                var request = _webRequestFactory.Create(url, headers);
                request.SendRequest();
                return request;
            }
        }
    }
}