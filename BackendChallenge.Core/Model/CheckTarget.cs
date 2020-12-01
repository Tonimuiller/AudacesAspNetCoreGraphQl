using System;
using System.Collections.Generic;
using System.Linq;

namespace BackendChallenge.Core.Model
{
    public class CheckTarget
    {
        public int Id { get; set; }
        public DateTime Creation { get; set; }
        public int Target { get; set; }
        public IEnumerable<int> Sequence { get; set; }
        public string SequenceSerialized 
        {
            get => string.Join("|", this.Sequence);
            set
            {
                if (string.IsNullOrEmpty(value?.Trim()))
                {
                    this.Sequence = null;
                    return;
                }

                this.Sequence = value.Split("|").Select(el => Convert.ToInt32(el));
            }
        }
    }
}
