﻿namespace AngleSharp.Html.InputTypes
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Services.Media;
    using System;
    using System.Threading.Tasks;

    class ImageInputType : BaseInputType
    {
        #region Fields

        IImageInfo _img;

        #endregion

        #region ctor

        public ImageInputType(IHtmlInputElement input, String name)
            : base(input, name, validate: true)
        {
            var inp = input as HtmlInputElement;
            var src = input.Source;

            if (src != null && inp != null)
            {
                var url = inp.HyperReference(src);
                var request = inp.CreateRequestFor(url);
                inp.LoadResource<IImageInfo>(request).
                    ContinueWith(FinishLoading);
            }
        }

        #endregion

        #region Properties

        public Int32 Width
        {
            get { return _img != null ? _img.Width : 0; }
        }

        public Int32 Height
        {
            get { return _img != null ? _img.Height : 0; }
        }

        #endregion

        #region Methods

        public override Boolean IsAppendingData(IHtmlElement submitter)
        {
            return Object.ReferenceEquals(submitter, Input) && !String.IsNullOrEmpty(Input.Name);
        }

        public override void ConstructDataSet(FormDataSet dataSet)
        {
            var name = Input.Name;
            var value = Input.Value;

            dataSet.Append(name + ".x", "0", Input.Type);
            dataSet.Append(name + ".y", "0", Input.Type);

            if (!String.IsNullOrEmpty(value))
                dataSet.Append(name, value, Input.Type);
        }

        #endregion

        #region Helpers

        void FinishLoading(Task<IImageInfo> m)
        {
            var inp = (HtmlInputElement)Input;

            if (m.IsCompleted && !m.IsFaulted)
                _img = m.Result;

            inp.FireLoadOrErrorEvent(m);
        }

        #endregion
    }
}
