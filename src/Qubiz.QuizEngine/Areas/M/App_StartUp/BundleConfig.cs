﻿using System;
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
            bundles.Add(angularMaterialBundle);

            Bundle angularBundle = new Bundle("~/Areas/M/angular", jsTransforms);
            angularBundle.Include("~/Areas/M/Scripts/angular.js");
            angularBundle.Include("~/Areas/M/Scripts/angular-aria.js");
            angularBundle.Include("~/Areas/M/Scripts/angular-animate.js");
            angularBundle.Include("~/Areas/M/Scripts/angular-material.js");
            angularBundle.Include("~/Areas/M/Scripts/angular-route.js");
            bundles.Add(angularBundle);

            Bundle appModuleBundle = new Bundle("~/Areas/M/module", jsTransforms);
            appModuleBundle.Include("~/Areas/M/App/app.module.js");
            bundles.Add(appModuleBundle);

            Bundle testsBundle = new Bundle("~/Areas/M/tests", jsTransforms);
            testsBundle.Include("~/Areas/M/App/Tests/Services/tests.service.js");
            testsBundle.Include("~/Areas/M/App/Tests/tests.controller.js");
            bundles.Add(testsBundle);


            Bundle controllerBundle = new Bundle("~/Areas/M/AngularControllers", jsTransforms);
            controllerBundle.Include("~/Areas/M/App/MainBarController/MainBar.controller.js");
            controllerBundle.Include("~/Areas/M/App/Controllers/questions.controller.js");
            bundles.Add(controllerBundle);

            Bundle serviceBundle = new Bundle("~/Areas/M/AngularServices", jsTransforms);
            serviceBundle.Include("~/Areas/M/App/Services/questions.service.js");
            bundles.Add(serviceBundle);
        }
    }
}
