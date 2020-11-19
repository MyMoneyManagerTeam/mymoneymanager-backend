using System;

namespace Application.Services.Jars.Dto
{
    public class OutputDtoDeleteJar
    {
        public Guid Id { get; set; }
        public Guid Owner { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public double Max { get; set; }
        public double Balance { get; set; }
    }
}