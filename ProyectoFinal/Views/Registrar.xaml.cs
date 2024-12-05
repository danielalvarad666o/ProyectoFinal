using ProyectoFinal.Models;
using ProyectoFinal.Service;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProyectoFinal.Views
{


    public partial class Registrar : ContentPage, INotifyPropertyChanged
    {




        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

      


        private string _name;
        private string _email;
        private string _password;
        private string _phone;
        private bool _isRegisterButtonEnabled;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
                CheckFields();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                CheckFields();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
                CheckFields();
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
                CheckFields();
            }
        }

        public bool IsRegisterButtonEnabled
        {
            get => _isRegisterButtonEnabled;
            set
            {
                _isRegisterButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        public Registrar()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void CheckFields()
        {
            IsRegisterButtonEnabled = !string.IsNullOrWhiteSpace(Name) &&
                                      !string.IsNullOrWhiteSpace(Email) &&
                                      !string.IsNullOrWhiteSpace(Password) &&
                                      !string.IsNullOrWhiteSpace(Phone);
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

            // Mostrar animación de carga y deshabilitar botón
          
            RegisterButton.IsEnabled = false;
            IsBusy = true;

            try
            {
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

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(userApiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var responseData = JsonSerializer.Deserialize<ResponseData>(responseContent);

                        if (responseData != null && responseData.User != null)
                        {
                            //string responseMessage =
                            //    $"¡Registro exitoso!\n\nNombre: {responseData.User.Name}\nCorreo: {responseData.User.Email}\nTeléfono: {responseData.User.Phone}\nFecha de Nacimiento: {responseData.User.BirthDate}\nEstado: {responseData.User.StatusId}";
                            //await DisplayAlert("Éxito", responseMessage, "OK");

                            
                            await DisplayAlert("Éxito", "Usuario Registrado Exitosamente", "OK");
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert("Error", "No se pudieron obtener los datos del usuario.", "OK");
                        }
                    }
                    else
                    {
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        await DisplayAlert("Error", $"Error del servidor: {response.StatusCode}\n{errorResponse}", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
            }
            finally
            {
                // Ocultar animación de carga y habilitar botón
                IsBusy = false;
               
                RegisterButton.IsEnabled = true;
            }
        }

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
    }
    




}

