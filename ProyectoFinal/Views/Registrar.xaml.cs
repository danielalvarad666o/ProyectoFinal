using ProyectoFinal.Models;
using ProyectoFinal.Service;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProyectoFinal.Views
{
    public partial class Registrar : ContentPage
    {
        private readonly AppDbContext _dbContext;

        public Registrar()
        {
            InitializeComponent();
            _dbContext = new AppDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<AppDbContext>());
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string name = NameEntry.Text;
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;
            string phone = PhoneEntry.Text;
            DateTime birthDate = BirthDatePicker.Date;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(phone))
            {
                await DisplayAlert("Error", "Por favor completa todos los campos.", "OK");
                return;
            }

            // Mostrar animación de carga
            var loadingIndicator = new ActivityIndicator { IsRunning = true };
            await this.DisplayLoadingAnimation(loadingIndicator);

            // Verificar y guardar los estados antes de registrar al usuario
            string statusesApiUrl = "http://127.0.0.1:8000/api/statuses";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(statusesApiUrl);
                    var statuses = JsonSerializer.Deserialize<Dictionary<string, ApiStatus>>(response);

                    if (statuses != null)
                    {
                        // Convertir los estados obtenidos a entidades Status
                        // Convertir los estados obtenidos a entidades Status
                        var newStatuses = statuses.Values.Select(status => new Status
                        {
                            Id = status.Id,
                            Type = status.Type
                        }).ToList();

                        // Guardar todos los estados en la base de datos
                        _dbContext.Statuses.AddRange(newStatuses);
                        

                        // Guardar cambios
                        await _dbContext.SaveChangesAsync();


                  
                        
                    }
                }
            }
            catch (Exception ex)
            {
                string innerExceptionMessage = ex.InnerException?.Message ?? "No hay detalles adicionales.";
                await DisplayAlert("Error", $"Error al guardar estados: {ex.Message}\nDetalles: {innerExceptionMessage}", "OK");

                Console.WriteLine($"Error al guardar estados: {ex.Message}");
                Console.WriteLine($"Detalles: {innerExceptionMessage}");
            }


            finally
            {
                // Detener animación de carga
                await this.HideLoadingAnimation(loadingIndicator);
            }

            // Crear el nuevo usuario
            var newUser = new
            {
                name,
                email,
                password,
                phone,
                birth_date = birthDate.ToString("yyyy-MM-dd")
            };

            string userApiUrl = "http://127.0.0.1:8000/api/users/cUser";
            var json = JsonSerializer.Serialize(newUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(userApiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var responseData = JsonSerializer.Deserialize<ResponseData>(responseContent);

                        if (responseData != null && responseData.User != null)
                        {
                            // Guardar en SQLite
                            var user = new User
                            {
                                Id = responseData.User.Id,
                                Name = responseData.User.Name,
                                Email = responseData.User.Email,
                                Phone = responseData.User.Phone,
                                BirthDate = DateTime.Parse(responseData.User.BirthDate),
                                StatusId = responseData.User.StatusId
                            };

                            _dbContext.Users.Add(user);
                            await _dbContext.SaveChangesAsync();

                            string responseMessage =
                                $"¡Registro exitoso!\n\nNombre: {responseData.User.Name}\nCorreo: {responseData.User.Email}\nTeléfono: {responseData.User.Phone}\nFecha de Nacimiento: {responseData.User.BirthDate}\nEstado: {responseData.User.StatusId}";
                            await DisplayAlert("Éxito", responseMessage, "OK");
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Error", "No se pudieron obtener los datos del usuario.", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Error", $"Error del servidor: {response.StatusCode}", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
        }

        private class ApiStatus
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("type")]
            public string Type { get; set; }
        }

        private class ResponseData
        {
            [JsonPropertyName("message")]
            public string Message { get; set; }

            [JsonPropertyName("user")]
            public ApiUser User { get; set; }
        }

        private class ApiUser
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("email")]
            public string Email { get; set; }

            [JsonPropertyName("phone")]
            public string Phone { get; set; }

            [JsonPropertyName("birth_date")]
            public string BirthDate { get; set; }

            [JsonPropertyName("status_id")]
            public int StatusId { get; set; }
        }

        private async Task DisplayLoadingAnimation(ActivityIndicator loadingIndicator)
        {
            loadingIndicator.IsVisible = true;
            this.Content = loadingIndicator;
            await Task.Delay(500); // Breve retraso para mostrar animación
        }

        private async Task HideLoadingAnimation(ActivityIndicator loadingIndicator)
        {
            loadingIndicator.IsVisible = false;
            await Task.Delay(500); // Breve retraso para ocultar animación
        }
    }
}
