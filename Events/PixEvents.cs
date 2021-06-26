using System;

namespace Events {
    
    public interface IPayPix {
        Guid Id { get; }
        long Value { get; }
    }
    
    public interface IPixPayed {
        IPayPix Pix { get;  }
        
        DateTime Time { get;  }
    }
}