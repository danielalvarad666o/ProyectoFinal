using ProyectoFinal.Service;
using Microsoft.Maui.Controls;
using System.Text;
using System.Text.Json;

namespace ProyectoFinal;

public partial class MainPage : ContentPage
{
    private string _email;
    private string _password;
    private bool _isRegisterButtonEnabled;
    private bool _isBusy;

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

    public bool IsRegisterButtonEnabled
    {
        get => _isRegisterButtonEnabled;
        set
        {
            _isRegisterButtonEnabled = value;
            OnPropertyChanged();
        }
    }

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged();
        }
    }

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this; // Establece el contexto de enlace
    }

    private void CheckFields()
    {
        IsRegisterButtonEnabled =
            !string.IsNullOrWhiteSpace(Email) &&
            !string.IsNullOrWhiteSpace(Password);
    }

    public async void OnLogin(object sender, EventArgs e)
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            RegisterButton.IsEnabled = false;

            var loginData = new { email = Email, password = Password };
            string loginApiUrl = "http://127.0.0.1:8000/api/users/login";
            var json = JsonSerializer.Serialize(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using HttpClient client = new HttpClient();



                var response = await client.PostAsync(loginApiUrl, content);
            

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<LoginResponse>(responseContent);




                if (responseData != null)
                {

                    await DisplayAlert("Inicio de sesión exitoso",
                        $"Bienvenido {responseData.data.user.name}", "OK");

                   

                    await SecureStorage.Default.SetAsync("token",responseData.data.token);
                    await SecureStorage.Default.SetAsync("name", responseData.data.user.name);
                    

                    await SecureStorage.Default.SetAsync("phone", responseData.data.user.phone);
                   
                    string cumplaños=  responseData.data.user.birth_date;
                    await SecureStorage.Default.SetAsync("birth_date", cumplaños);
                    string id = Convert.ToString( responseData.data.user.id);
                   await SecureStorage.Default.SetAsync("id", id);



                    await Navigation.PushAsync(new ProyectoFinal.Views.lobby());


                }
                else
                {
                    await DisplayAlert("Error", responseData?.msg ?? "Error desconocido.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", $"Error del servidor: {response.StatusCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
            RegisterButton.IsEnabled = true;
        }
    }

    private async void OnRegister(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new ProyectoFinal.Views.Registrar());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo abrir la página de registro: {ex.Message}", "OK");
        }
    }

    public class LoginResponse
    {
        public int status { get; set; }
        public string msg { get; set; }
        public LoginData data { get; set; }
    }

    public class LoginData
    {
        public User user { get; set; }
        public string token { get; set; }
    }

     public  class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string birth_date { get; set; }
        public int statusId { get; set; }
    }
}
