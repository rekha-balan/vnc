﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;

namespace PluralsightPrismDemo.Infrastructure
{
    public class PersonUpdatedEvent : CompositePresentationEvent<string> { }
}