using System.Net;
using Wall_e.api;

namespace Wall_e.infra;

public class HttpIo : IO
{
    private readonly HttpListener _listener;
    private HttpListenerRequest? _currentRequest;
    private HttpListenerResponse? _currentResponse;

    public HttpIo()
    {
        _listener = new HttpListener();
        _listener.Start();
        _listener.Prefixes.Add("http://localhost:8080/");
    }

    public void DoWrite(string text)
    {
        if (_currentResponse == null) throw new InvalidOperationException("Cannot write to a null stream");

        _currentResponse.ContentType = "text/plain";
        using var writer = new StreamWriter(_currentResponse.OutputStream);
        writer.WriteLine(text);
        writer.Close();
        _currentResponse.Close();
    }

    public string? DoRead()
    {
        _listener.BeginGetContext(ListenHttp, _listener);
        if (_currentRequest == null) return null;
        if (!_currentRequest.HasEntityBody) return _currentRequest.Url?.PathAndQuery.TrimStart('/');

        using var body = _currentRequest.InputStream;
        using var reader = new StreamReader(body, _currentRequest.ContentEncoding);
        return reader.ReadToEnd();
    }

    private void ListenHttp(IAsyncResult result)
    {
        while (true)
            if (_listener.IsListening)
            {
                var currentContext = _listener.EndGetContext(result);
                _currentRequest = currentContext.Request;
                _currentResponse = currentContext.Response;
            }
        // ReSharper disable once FunctionNeverReturns
    }
}