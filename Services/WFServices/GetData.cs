﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WFServices
{

    public sealed class GetData : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<string> Text { get; set; }
        public InArgument<string> Status { get; set; }
        public OutArgument<string> ID { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string text = context.GetValue(this.Text);
            string ID = context.GetValue(this.ID);
            //context.SetValue(Status, "Pending");
        }
    }
}
