﻿using System;
using System.Threading.Tasks;

namespace SpectrumTV.Services.ApiService
{
    /// <summary>
    /// Interface methods to api calls
    /// </summary>

    public interface IApiService
    {
        Task<TResult> GetAsync<TResult>(string uri);
        Task<TResult> PostAsync<TResult>(string uri, TResult data);
    }
}
