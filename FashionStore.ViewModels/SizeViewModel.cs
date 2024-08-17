using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionStore.ViewModels;

public class SizeViewModel
{
    public int Id { get; set; }
    public ProductSize ProductSize { get; set; }
}

public enum ProductSize
{
    small,
    medium,
    large,
    xlarge,
    xxlarge
}
