﻿// MvxPhoneTargetBindingFactoryRegistry.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
// 
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using Cirrious.MvvmCross.Binding.Bindings.Target;
using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;
using Cirrious.MvvmCross.BindingEx.WindowsPhone.MvxBinding.Target;

namespace Cirrious.MvvmCross.BindingEx.WindowsPhone.MvxBinding
{
    public class MvxPhoneTargetBindingFactoryRegistry : MvxTargetBindingFactoryRegistry
    {
        protected override bool TryCreateReflectionBasedBinding(object target, string targetName,
                                                                out Binding.Bindings.Target.IMvxTargetBinding binding)
        {
            if (TryCreatePropertyDependencyBasedBinding(target, targetName, out binding))
            {
                return true;
            }

            return base.TryCreateReflectionBasedBinding(target, targetName, out binding);
        }

        private bool TryCreatePropertyDependencyBasedBinding(object target, string targetName,
                                                             out IMvxTargetBinding binding)
        {
            if (target == null)
            {
                binding = null;
                return false;
            }

            var dependencyProperty = target.GetType().FindDependencyProperty(targetName);
            if (dependencyProperty == null)
            {
                binding = null;
                return false;
            }

            var actualProperty = target.GetType().FindActualProperty(targetName);
            var actualPropertyType = actualProperty == null ? typeof (object) : actualProperty.PropertyType;

            binding = new MvxDependencyPropertyTargetBinding(target, targetName, dependencyProperty, actualPropertyType);
            return true;
        }
    }
}