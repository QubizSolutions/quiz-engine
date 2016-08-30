using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace Qubiz.QuizEngine.Areas.M
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            IBundleTransform[] jsTransforms = new IBundleTransform[0];
            IBundleTransform[] cssTransforms = new IBundleTransform[0];

            Bundle angularMaterialBundle = new Bundle("~/Areas/M/angular-material", cssTransforms);
            angularMaterialBundle.Include("~/Areas/M/Content/angular-material/angular-material.css");
            angularMaterialBundle.Include("~/Areas/M/Content/angular-material/admins.style.css");
            angularMaterialBundle.Include("~/Areas/M/Content/sections.css");
            angularMaterialBundle.Include("~/Areas/M/App/MenuBar.StyleSheet.css");
            bundles.Add(angularMaterialBundle);

            Bundle materialLiteBundle = new Bundle("~/Content/mdl", cssTransforms);
            materialLiteBundle.Include("~/Content/mdl-v1.1.2/material.css");
			materialLiteBundle.Include("~/Areas/M/Content/table.style.css");
            bundles.Add(materialLiteBundle);

            Bundle angularBundle = new Bundle("~/Areas/M/angular", jsTransforms);
            angularBundle.Include("~/Areas/M/Scripts/angular.js");
            angularBundle.Include("~/Areas/M/Scripts/angular-aria.js");
            angularBundle.Include("~/Areas/M/Scripts/angular-animate.js");
            angularBundle.Include("~/Areas/M/Scripts/angular-material.js");
            angularBundle.Include("~/Areas/M/Scripts/angular-route.js");
            angularBundle.Include("~/Content/mdl-v1.1.2/material.js");
            bundles.Add(angularBundle);

            Bundle appModuleBundle = new Bundle("~/Areas/M/module", jsTransforms);
            appModuleBundle.Include("~/Areas/M/App/app.module.js");
            bundles.Add(appModuleBundle);

            Bundle controllerBundle = new Bundle("~/Areas/M/AngularControllers", jsTransforms);
            controllerBundle.Include("~/Areas/M/App/mainbar/mainbar.controller.js");
            controllerBundle.Include("~/Areas/M/App/ckeditor/ck-editor.directive.js");
            controllerBundle.Include("~/Areas/M/App/tests/tests.controller.js");
            controllerBundle.Include("~/Areas/M/App/questions/questions.controller.js");
            controllerBundle.Include("~/Areas/M/App/questions/questions-edit.controller.js");
            controllerBundle.Include("~/Areas/M/App/questions/questions-add.controller.js");
            controllerBundle.Include("~/Areas/M/App/sections/sections.controller.js");
            controllerBundle.Include("~/Areas/M/App/sections/section-save.controller.js");
            controllerBundle.Include("~/Areas/M/App/admins/admins.controller.js");
            controllerBundle.Include("~/Areas/M/App/admins/admin-save.controller.js");
            bundles.Add(controllerBundle);

            Bundle serviceBundle = new Bundle("~/Areas/M/AngularServices", jsTransforms);
            serviceBundle.Include("~/Areas/M/App/common/services/guids.service.js");
            serviceBundle.Include("~/Areas/M/App/common/services/httpService.service.js");
            serviceBundle.Include("~/Areas/M/App/tests/tests.service.js");
            serviceBundle.Include("~/Areas/M/App/questions/questions.service.js");
            serviceBundle.Include("~/Areas/M/App/sections/sections.service.js");
            serviceBundle.Include("~/Areas/M/App/admins/admins.service.js");
            bundles.Add(serviceBundle);
		}
    }
}
