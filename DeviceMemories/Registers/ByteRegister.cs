﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECore.DeviceMemories
{
    public class ByteRegister : MemoryRegister
    {
        private byte internalValue
        {
            get { return (byte)_internalValue; }
            set { _internalValue = value; }
        }

        internal ByteRegister(DeviceMemory memory, uint address, string name) : base(memory, address, name) 
        {
            this.internalValue = 0;
        }

#if INTERNAL
        public
#else
        internal
#endif
        override MemoryRegister Set(object value)
        {
            byte castValue;
            if(!value.GetType().Equals(typeof(byte))) 
            {
                try
                {
                    castValue = (byte)((int)value & 0xFF);
                    if ((int)value != (int)castValue)
                        throw new Exception("Cast to byte resulted in loss of information");
                }
                catch (InvalidCastException)
                {
                    throw new Exception("Cannot set ByteRegister with that kind of type (" + value.GetType().Name + ")");
                }
            }
            else
                castValue = (byte)value;
            this.internalValue = castValue;
            CallValueChangedCallbacks();
            return this;
        }

#if INTERNAL
        public
#else
        internal
#endif
        override object Get() { return this.internalValue; }

#if INTERNAL
        public
#else
        internal
#endif
        byte GetByte() { return this.internalValue; }
        
#if INTERNAL
        public
#else
        internal
#endif  
        new ByteRegister Read() { return (ByteRegister)base.Read(); }

        public override int MaxValue { get { return 255; } }
    }
}
