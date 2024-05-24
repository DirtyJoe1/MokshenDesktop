using MokshenDesktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Collections.ObjectModel;

namespace MokshenDesktop.Resources.Services
{
    public class Repository : HttpClientMK
    {
        private readonly string RegistrationUrl = "/api/authentication/registration";
        private readonly string LoginUrl = "/api/authentication/login";
        private readonly string ExerciseUrl = "/api/exercise";
        private readonly string CreateExerciseUrl = "/api/exercise/create-exercise";
        private readonly string ExerciseByCategoryUrl = "/api/exercise/exercises-by-category?category=";
        private readonly string CheckExercisesUrl = "/api/exercise/check-exercises?category=";
        private readonly HttpClient _httpClient;
        public Repository()
        {
            _httpClient = GetHttpClient();
        }
        public async Task<HttpResponseMessage> PostAuthenticationRegister(UserRegistration login)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(RegistrationUrl, login);
            return responseMessage;
        }

        public async Task<HttpResponseMessage> PostAuthenticationLogin(UserLogin login)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(LoginUrl, login);
            dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(await responseMessage.Content.ReadAsStringAsync());
            TokenStorage.Token = responseData.token;
            TokenStorage.Role = responseData.role;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenStorage.Token);
            return responseMessage;
        }
        public async Task<ObservableCollection<Exercise>> GetExercisesAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenStorage.Token);
            ObservableCollection<Exercise> exercises = null;
            var response = await _httpClient.GetAsync(ExerciseUrl);
            if (response.IsSuccessStatusCode)
            {
                exercises = await response.Content.ReadFromJsonAsync<ObservableCollection<Exercise>>();
            }
            return exercises;
        }
        public async Task<HttpResponseMessage> CreateExercise(Exercise exercise)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenStorage.Token);
            return await _httpClient.PostAsJsonAsync(CreateExerciseUrl, exercise);
        }
        public async Task<HttpResponseMessage> DeleteExercise(Guid id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenStorage.Token);
            return await _httpClient.DeleteAsync(ExerciseUrl + "/" + id.ToString());
        }
        public async Task<HttpResponseMessage> EditExercise(Exercise exercise)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenStorage.Token);
            return await _httpClient.PutAsJsonAsync(ExerciseUrl + "/" + exercise.Id.ToString(), exercise);
        }

        public async Task<ObservableCollection<Exercise>> GetExercisesByCategoryAsync(string category)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenStorage.Token);
            ObservableCollection<Exercise> exercises = null;
            var response = await _httpClient.GetAsync(ExerciseByCategoryUrl + category);
            if (response.IsSuccessStatusCode)
            {
                exercises = await response.Content.ReadFromJsonAsync<ObservableCollection<Exercise>>();
            }
            return exercises;
        }
        public async Task<HttpContent> CheckExercises(ObservableCollection<string> answers, string category)
        {
            var responseMessage = await _httpClient.PostAsJsonAsync(CheckExercisesUrl +  category, answers);
            return responseMessage.Content;
        }
    }
}
