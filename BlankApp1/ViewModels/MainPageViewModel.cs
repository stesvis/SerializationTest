using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BlankApp1.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        [Reactive] public string Json { get; set; }
        [Reactive] public string Json2 { get; set; }
        [Reactive] public string Error { get; set; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var data = new List<SystemSettingDTO>
            {
                new SystemSettingDTO { Id = 1, Name = "Test 1", Value = 100, Status = "Active", CreatedAt = DateTime.Now },
                new SystemSettingDTO { Id = 2, Name = "Test 2", Value = 200, Status = "Draft", CreatedAt = DateTime.Now },
            };

            try
            {
                Json = System.Text.Json.JsonSerializer.Serialize(data, new JsonSerializerOptions()
                {
                    MaxDepth = 0,
                    IgnoreNullValues = true,
                    IgnoreReadOnlyProperties = true,
                    PropertyNameCaseInsensitive = true,
                    IgnoreReadOnlyFields = true,
                    IncludeFields = false,
                    //PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                Json2 = Newtonsoft.Json.JsonConvert.SerializeObject(data, new JsonSerializerSettings()
                {
                    //MaxDepth = 1,
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
            }
            catch (Exception ex)
            {
                Error = ex.ToString();
            }
        }
    }

    public class SystemSettingDTO : BaseDTO
    {
        [Reactive] public string Name { get; set; }

        [Reactive] public object Value { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Status { get; set; }
    }

    public partial class BaseDTO : ReactiveObject
    {
        public int Id { get; set; }
    }
}