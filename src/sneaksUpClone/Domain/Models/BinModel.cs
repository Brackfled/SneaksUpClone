using System;
using Iyzipay;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;
public class BinModel: BaseRequest
{
    public string BinNumber { get; set; }

    public override string ToPKIRequestString()
    {
        return ToStringRequestBuilder.NewInstance().AppendSuper(base.ToPKIRequestString())
            .Append("binNumber", BinNumber)
            .GetRequestString();
    }
}
