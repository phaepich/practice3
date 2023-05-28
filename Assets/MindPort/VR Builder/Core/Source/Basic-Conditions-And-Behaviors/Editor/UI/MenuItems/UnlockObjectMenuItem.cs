﻿using System;
using VRBuilder.Core.Behaviors;
using VRBuilder.Editor.UI.StepInspector.Menu;

namespace VRBuilder.Editor.UI.Behaviors
{
    /// <inheritdoc />
    [Obsolete("Locking scene objects is obsoleted, consider using the 'Unlocked Objects' list in the Step window.")]
    public class UnlockObjectMenuItem : MenuItem<IBehavior>
    {
        /// <inheritdoc />
        public override string DisplayedName { get; } = "Environment/Unlock Object";

        /// <inheritdoc />
        public override IBehavior GetNewItem()
        {
            return new UnlockObjectBehavior();
        }
    }
}
