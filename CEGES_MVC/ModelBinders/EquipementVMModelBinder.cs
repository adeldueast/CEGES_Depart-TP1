using CEGES_Models;
using CEGES_Models.Enums;
using CEGES_MVC.ViewModels.EquipementVMs;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CEGES_MVC.ModelBinders
{
    public class EquipementVMEntityBinderProvider : IModelBinderProvider
    {

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType != typeof(EquipementVM))
            {
                return null;
            }
            var subclasses = new[] { typeof(EquipementConstantVM), typeof(EquipementLineaireVM), typeof(EquipementRelatifVM) };
            var binders = new Dictionary<Type, (ModelMetadata, IModelBinder)>();
            foreach (var type in subclasses)
            {
                var modelMetadata = context.MetadataProvider.GetMetadataForType(type);
                binders[type] = (modelMetadata, context.CreateBinder(modelMetadata));
            }
            return new EquipementVMModelBinder(binders);
        }
    }

    public class EquipementVMModelBinder : IModelBinder
    {
        private Dictionary<Type, (ModelMetadata, IModelBinder)> binders;

        public EquipementVMModelBinder(Dictionary<Type, (ModelMetadata, IModelBinder)> binders)
        {
            this.binders = binders;
        }
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelTypeName = ModelNames.CreatePropertyModelName(bindingContext.ModelName, nameof(EquipementVM.Type));
            var modelTypeValue = bindingContext.ValueProvider.GetValue(modelTypeName).FirstValue;
            IModelBinder modelBinder;
            ModelMetadata modelMetadata;
            if (modelTypeValue == TypeEquipmentEnumeration.Constant)
            {
                (modelMetadata, modelBinder) = binders[typeof(EquipementConstantVM)];
            }
            else if (modelTypeValue == TypeEquipmentEnumeration.Lineaire)
            {
                (modelMetadata, modelBinder) = binders[typeof(EquipementLineaireVM)];
            }
            else if (modelTypeValue == TypeEquipmentEnumeration.Relatif)
            {
                (modelMetadata, modelBinder) = binders[typeof(EquipementRelatifVM)];
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }
            var newBindingContext = DefaultModelBindingContext.CreateBindingContext(
                bindingContext.ActionContext,
                bindingContext.ValueProvider,
                modelMetadata,
                bindingInfo: null,
                bindingContext.ModelName);
            await modelBinder.BindModelAsync(newBindingContext);
            bindingContext.Result = newBindingContext.Result;
            if (newBindingContext.Result.IsModelSet)
            {
                // Setting the ValidationState ensures properties on derived types are correctly 
                bindingContext.ValidationState[newBindingContext.Result.Model] = new ValidationStateEntry
                {
                    Metadata = modelMetadata,
                };
            }
        }
    }
}
