using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProyectoFinal.Service
{
    internal class ApiRouter
    {
        private readonly string _baseUrl;

        public ApiRouter(string baseUrl)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
        }

        public string BaseUrl => _baseUrl;

        /// <summary>
        /// Verifica la conexión con el servidor especificado en _baseUrl.
        /// </summary>
        /// <returns>True si la conexión es exitosa, de lo contrario, False.</returns>
        public async Task<bool> CheckConnectionAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(5) })
                {
                    HttpResponseMessage response = await client.GetAsync(_baseUrl);
                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con el servidor: {ex.Message}");
                return false;
            }
        }
    }
}