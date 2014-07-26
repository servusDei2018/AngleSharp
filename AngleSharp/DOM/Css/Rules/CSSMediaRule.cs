﻿namespace AngleSharp.DOM.Css
{
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// Represents a CSS @media rule.
    /// </summary>
    sealed class CSSMediaRule : CSSConditionRule, ICssMediaRule
    {
        #region Fields

        IMediaList _media;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS @media rule.
        /// </summary>
        internal CSSMediaRule()
        {
            _media = MediaList.Empty;
            _type = CssRuleType.Media;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the media condition.
        /// </summary>
        public String ConditionText
        {
            get { return _media.MediaText; }
            set { _media.MediaText = value; }
        }

        /// <summary>
        /// Gets a list of media types for this rule.
        /// </summary>
        public IMediaList Media
        {
            get { return _media; }
            internal set { _media = value; }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = rule as CSSMediaRule;
            _media = newRule._media;
        }

        internal override Boolean IsValid(IWindow window)
        {
            //TODO
            return true;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Format("@media {0} {{{1}{2}}}", _media.MediaText, Environment.NewLine, Rules.ToCss());
        }

        #endregion
    }
}
