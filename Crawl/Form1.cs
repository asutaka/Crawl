using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crawl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindingList<PictureObject> list = new BindingList<PictureObject>();
            list.Add(new PictureObject("Image0", @"https://i.imgur.com/tHyzhwKb.jpg"));
            list.Add(new PictureObject("Image1", @"https://i.imgur.com/tHyzhwKb.jpg"));
            list.Add(new PictureObject("Image2", @"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEcAAAAOCAIAAADyhZbsAAAEK0lEQVRIid2WTUgyaxTHT03zmsVQmH1JgX0vyopRSCQpsu+iwiiiclEQtHAhEQMFrdq2CKuFSBkhLewDKiQkLGoZBZrIVBJJC/ugRlpk9Wp0F4/vXK8698KFy4X3rJ758f8fnjPPOfNMAsMw8NtF4v+9gf8k/rmq4eHhjIyM5ubmy8tLREKh0M9fwcriQq5YX18Xi8V+v/9f7DiuNxoyv8JsNvP5fJVKZbFYWKjX6wFgfn6+qakpPz//6emJYRiCINh0SqUSKWPhzc2NWq0WCoUYhmk0GjYnTdMEQbCES6bRaBISEkiSdDgcXF4uGK7K5/MJhUKVSmUymTAMM5lMiMtksoqKCoZh7HY7AGxubjIMc3p6enBwYDQaAaClpQUpY+HT01NPT4/BYDCbzQCwtLSElENDQwRBeDwe9BhXZrFYAGBiYqKtrU0mk7HbjfJywXAHbm9vv7y86PX67u7u2tra1dVVxF0ul1QqBYDy8nIAuLq6AoDi4mKpVPry8gIAXV1dSBkLk5KSVlZW+vr60Ba/v78B4PHx0WKxjIyMZGRkIGNcmcvlAgCKokQikdvtRspYLxcMV7Wzs1NVVZWbmwsAEokEJQWA1NTUzMxMAPjx4wcAfHx8sE6bzYZhWHt7e2R/R8FAIDA6Orq7u1tZWalWq9EABIPBhYUFpVJ5eHjIJSsrKwMAs9m8sbHR1NTEDk+sNy4Md6BQKBwaGkJrnU6HYRhaa7Xazs7O29vbubk5ADAajYjf3d3hOM4OVVxI07RIJAIADMNOTk7YlhYIBJubmxMTE3w+3+l0xpUxDEOSJADw+XyXy8Xl5YLhs/L7/ehM0JmmpKSg9fT09NvbW0FBweTkJI7j9fX1iB8dHQWDwdbW1siDioLv7+8+n4/H4319fQ0MDKDmpGm6o6OjoaGBoqj39/f9/f24svPzc9TtHx8fXq8XJYz1csFwVWlpaaFQCK0vLi5KSkrQOjk5eWtry263p6en9/b2sr2LzlqlUkVWFQXFYrHT6by/vz85OfH5fGhsQqFQdnY2ALy+vqLK48p0Ol1iYuLx8XFNTc3s7CxKGOvlguGqampq0FB6vV63293Y2Bi53bW1tc/Pz6mpKZacnZ3l5eWVlpZGymJhfn4+AKD3l5WVBQCFhYWBQAAArFYrAKBPUazs+vp6cHBQIpFUV1c7nU6ULa43LgzPldVqxXF8cXFRoVDweDy328329/LyMgDMz89HjhCPxxsbG2P+GlHQ4/Fsb2/39/cDgEKhQHfdzMxMUVGR0WgUCARyuZxLplQqSZI0GAzp6ekKhQIljPVywT9vYb1en5OTIxAI2MsKBUVR4+PjUQVUVlbabLa/h3t7ewRB5OXlURT18PCA4PPzs1arJQhCLpfTNM0lczgcJEniOF5XV8e+4lgvF0xgfse/2z8AsOr+k/ZXay4AAAAASUVORK5CYII="));
            gridView1.OptionsView.RowAutoHeight = true;
            gridView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent;
            gridControl1.DataSource = list;

            list.ListChanged += (s, args) => {
                if (args.PropertyDescriptor.Name == "Image")
                    gridView1.LayoutChanged();
            };
        }
    }
    public class PictureObject : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public Image Image { get; set; }
        public string Test { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public PictureObject(string name, string url)
        {
            Test = "abc";
            Name = name;
            Image = ResourceImageHelperCore.CreateImageFromResources("DevExpress.XtraEditors.Images.loading.gif", typeof(BackgroundImageLoader).Assembly);
            BackgroundImageLoader bg = new BackgroundImageLoader();
            bg.Loaded += (s, e) =>
            {
                Image = bg.Result;
                if (!(Image is Image)) Image = ResourceImageHelperCore.CreateImageFromResources("DevExpress.XtraEditors.Images.Error.png", typeof(BackgroundImageLoader).Assembly);
                PropertyChanged(this, new PropertyChangedEventArgs("Image"));
                //bg.Dispose();
            };
            bg.Load(url);
        }
    }
}
