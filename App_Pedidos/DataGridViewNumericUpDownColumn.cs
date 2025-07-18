using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace App_Pedidos
{
    public class DataGridViewNumericUpDownColumn : DataGridViewColumn
    {
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public decimal Value { get; set; }

        public DataGridViewNumericUpDownColumn()
            : base(new DataGridViewNumericUpDownCell())
        {
            Minimum = 1;
            Maximum = 100;
            Value = 1;
        }

        public override object Clone()
        {
            var col = (DataGridViewNumericUpDownColumn)base.Clone();
            col.Minimum = this.Minimum;
            col.Maximum = this.Maximum;
            col.Value = this.Value;
            return col;
        }
    }

    public class DataGridViewNumericUpDownCell : DataGridViewTextBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            NumericUpDownEditingControl ctl = DataGridView.EditingControl as NumericUpDownEditingControl;
            DataGridViewNumericUpDownColumn col = OwningColumn as DataGridViewNumericUpDownColumn;

            ctl.Minimum = col.Minimum;
            ctl.Maximum = col.Maximum;
            ctl.Value = col.Value;
        }

        public override Type EditType => typeof(NumericUpDownEditingControl);

        public override Type ValueType => typeof(decimal);

        public override object DefaultNewRowValue => 1;
    }

    public class NumericUpDownEditingControl : NumericUpDown, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;

        public object EditingControlFormattedValue
        {
            get => this.Value.ToString();
            set
            {
                if (decimal.TryParse(value.ToString(), out decimal val))
                    this.Value = val;
            }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context) => EditingControlFormattedValue;

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle) => this.Font = dataGridViewCellStyle.Font;

        public int EditingControlRowIndex { get => rowIndex; set => rowIndex = value; }

        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey) => true;

        public void PrepareEditingControlForEdit(bool selectAll) { }

        public bool RepositionEditingControlOnValueChange => false;

        public DataGridView EditingControlDataGridView { get => dataGridView; set => dataGridView = value; }

        public bool EditingControlValueChanged { get => valueChanged; set => valueChanged = value; }

        public Cursor EditingPanelCursor => base.Cursor;

        protected override void OnValueChanged(EventArgs eventargs)
        {
            valueChanged = true;
            EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }

}
