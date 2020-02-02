using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Helpers
{
    public class RavenIdModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var value = valueProviderResult.FirstValue;

            if (string.IsNullOrEmpty(value))
            {
                return Task.CompletedTask;
            }

            if (!int.TryParse(value, out var id))
            {
                bindingContext.ModelState.TryAddModelError(
                    modelName, "Id must be an integer.");

                return Task.CompletedTask;
            }

            var dbModelType = bindingContext.ModelType.GetGenericArguments()[0];

            var outputModelType = typeof(RavenId<>).MakeGenericType(dbModelType);

            var model = Activator.CreateInstance(outputModelType, IdHelper.ForModel(dbModelType, value));
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}