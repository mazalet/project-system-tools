﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.ProjectSystem.LogModel;

namespace Microsoft.VisualStudio.ProjectSystem.Tools.BuildLogExplorer.ViewModel
{
    internal sealed class EvaluatedLocationViewModel : BaseViewModel
    {
        private readonly EvaluatedLocation _evaluatedLocation;
        private string _text;
        private List<object> _children;
        private SelectedObjectWrapper _properties;

        public override string Text => _text ?? (_text = $"{_evaluatedLocation.File}{(_evaluatedLocation.Line == null ? string.Empty : $" {_evaluatedLocation.Line}")} [{FormatTime(_evaluatedLocation.Time)}]");

        public override IEnumerable<object> Children => _children ?? (_children = GetChildren());

        public override SelectedObjectWrapper Properties => _properties ?? (_properties =
            new SelectedObjectWrapper(
                _evaluatedLocation.ElementName,
                _evaluatedLocation.Kind.ToString(),
                null,
                new Dictionary<string, IDictionary<string, string>> {
                    {"General", new Dictionary<string, string>
                        {
                            {"Description", _evaluatedLocation.ElementDescription},
                        }
                     }
                }));

        public EvaluatedLocationViewModel(EvaluatedLocation evaluatedLocation)
        {
            _evaluatedLocation = evaluatedLocation;
        }

        private List<object> GetChildren() => ((IEnumerable<object>)_evaluatedLocation.Children.Select(location => new EvaluatedLocationViewModel(location))).ToList();
    }
}
