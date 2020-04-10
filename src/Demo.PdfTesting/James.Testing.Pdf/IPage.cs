﻿using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Collections.Generic;

namespace James.Testing.Pdf
{
    public interface IPage
    {
        IList<Line> Lines { get;  }
    }
}