using ProyectoFinal.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace ProyectoFinal.Controladores
{
     class registrarClase
    {
       

        private const string BaseUrl = "http://127.0.0.1:8000/api/";

        // Método para inscribir a un usuario en una clase
        public async Task<bool> InscribirClaseAsync(int userId, int claseId)
        {
            try
            {
                string url = $"{BaseUrl}inscribiraclase";
                using HttpClient client = new HttpClient();
                var payload = new
                {
                    user_id = userId,
                    horario_clase_id = claseId
                };

                string json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    return true; // Inscripción exitosa
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    await MostrarAlertaAsync("Error", $"No se pudo inscribir: {errorResponse}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await MostrarAlertaAsync("Error", $"Error en la inscripción: {ex.Message}");
                return false;
            }
        }

        // Método para obtener las clases inscritas de un usuario
        public async Task<ObservableCollection<ClaseDatainfo>> ObtenerClasesInscritasAsync(int userId)
        {
            try
            {
                string url = $"{BaseUrl}usuarios/{userId}/clases";
                using HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(url);

                var apiResponse = JsonSerializer.Deserialize<ApiResponseinfo<List<ClaseDatainfo>>>(response);

                if (apiResponse?.success == true)
                {
                    return new ObservableCollection<ClaseDatainfo>(apiResponse.data);
                }
                else
                {
                    await MostrarAlertaAsync("Error", "No se pudieron cargar las clases.");
                    return new ObservableCollection<ClaseDatainfo>();
                }
            }
            catch (Exception ex)
            {
                await MostrarAlertaAsync("Error", $"Error al cargar clases: {ex.Message}");
                return new ObservableCollection<ClaseDatainfo>();
            }
        }

        // Método reutilizable para mostrar alertas
        private async Task MostrarAlertaAsync(string titulo, string mensaje)
        {
            await Application.Current.MainPage?.DisplayAlert(titulo, mensaje, "OK");
        }
    }

    // Modelos
    public class ApiResponseinfo<T>
    {
        public bool success { get; set; }
        public T data { get; set; }
    }

    public class ClaseDatainfo
    {
        public int id { get; set; }
        public string dia_semana { get; set; }
        public string hora_inicio { get; set; }
        public string hora_fin { get; set; }
        public ClaseInfo clase { get; set; }
        public EntrenadorInfo entrenador { get; set; }
    }

    public class ClaseInfo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int duracion_min { get; set; }
        public int max_participantes { get; set; }
    }

    public class EntrenadorInfo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string especialidad { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
    }
}
