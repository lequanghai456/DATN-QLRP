using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ESEIM.Utils
{
    public class Paging
    {
        public string search { set; get; }
        public int itemsPerPage { set; get; }
        public int bigCurrentPage { set; get; }
    }
    public class Pagination
    {
        public int bigTotalItems { set; get; }
        public int bigCurrentPage { set; get; }
        public dynamic Data { set; get; }
    }
    public class DataDynamic
    {
        public List<List<dynamic>> Data { set; get; }
        public DataDynamic(int x)
        {
            Data = new List<List<dynamic>>();
            for (int i = 0; i < x; i++)
            {
                Data.Add(new List<dynamic>());
            }
        }
    }
    public class TempSub
    {
        public int[] IdI { set; get; } 
        public string[] IdS { set; get; }
        public string Pin { set; get; }
    }
    public class Order
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }
    public class Search
    {
        public string value { get; set; }
        public string Regex { get; set; }
    }
    public class Column
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; } 
        public bool Orderable { get; set; }
        public Search Search { get; set; }
    }
    public class JTableModel
    {
     
        public int Draw { get; set; }
        public List<Column> Columns { get; set; }
        public List<Order> Order { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
         
        public Search search { get; set; } 
        public string  QueryOrderBy
        {
            get
            {
                try
                {
                    var lstSort = new List<string>();
                    foreach (var item in GetSortedColumns())
                    {
                        var sdir = "";
                        sdir = item.Direction.ToString().Equals(SortingDirection.Ascending.ToString()) ? "" : "DESC";
                        lstSort.Add(string.Format("{0} {1}", item.PropertyName, sdir));

                    }
                    return string.Join(",", lstSort);
                }
                catch (Exception)
                {

                    return "";
                }
            }

            

        }
        public int CurrentPage
        {
            get
            {
                var d = (double)((double)Start / (double)Length);
                return (int)Math.Ceiling(d) + 1;
            }
        }
        private IEnumerable<SortedColumn> GetSortedColumns()
        {
            if (!Order.Any())
            {
                return new ReadOnlyCollection<SortedColumn>(new List<SortedColumn>());
            } 
            var sortedColumns = Order.Select(item => new SortedColumn(string.IsNullOrEmpty(Columns[item.Column].Name) ? Columns[item.Column].Data : Columns[item.Column].Name, item.Dir)).ToList();
            return sortedColumns.AsReadOnly();
        }
    }

    public class SortedColumn
    {
        private const string Ascending = "asc";
        public SortedColumn(string propertyName, string sortingDirection)
        {
            PropertyName = propertyName;
            Direction = sortingDirection.Equals(Ascending) ? SortingDirection.Ascending : SortingDirection.Descending;
        }
        public string PropertyName { get; private set; }
        public SortingDirection Direction { get; private set; }
        public override int GetHashCode()
        {
            var directionHashCode = Direction.GetHashCode();
            return PropertyName != null ? PropertyName.GetHashCode() + directionHashCode : directionHashCode;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (GetType() != obj.GetType())
            {
                return false;
            }
            var other = (SortedColumn)obj;
            if (other.Direction != Direction)
            {
                return false;
            }
            return other.PropertyName == PropertyName;
        }
    }
    public enum SortingDirection
    {
        Ascending,
        Descending
    }
}
