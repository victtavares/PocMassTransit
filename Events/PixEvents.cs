using System;

namespace Events {
    
    public interface IPayPix {
        Guid Id { get;  }
        long Value { get; }
    }
}