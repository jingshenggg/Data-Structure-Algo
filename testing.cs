    protected void subpanelCompensationPenaltyCharge_DoCalculation_OnChanged(object sender, EventArgs e)
    {
        OLicenseCompensationPenaltyCharges item = subpanelCompensationPenaltyCharge.SessionObject as OLicenseCompensationPenaltyCharges;
        subpanelCompensationPenaltyCharge.ObjectPanel.BindControlsToObject(item);

        if (sender is UIFieldTextBox)
        {
            UIFieldTextBox tb = (UIFieldTextBox)sender;
            if (tb.ID == "subpanelCompensationPenaltyCharge_tbUnitPrice")
                item.RefreshAmounts(true, false, false, false);
            else if (tb.ID == "subpanelCompensationPenaltyCharge_tbQuantity")
                item.RefreshAmounts(false, true, false, false);
            else if (tb.ID == "subpanelCompensationPenaltyCharge_tbAmount")
                item.RefreshAmounts(false, false, true, false);
            else if (tb.ID == "subpanelCompensationPenaltyCharge_tbWaivedAmount")
                item.RefreshAmounts(false, false, false, true);
        }
        else if (sender is UIFieldCheckBox)
        {
            UIFieldCheckBox cb = (UIFieldCheckBox)sender;
            if (cb.ID == "subpanelCompensationPenaltyCharge_cbTaxInclusiveIndicator")
                item.CalculateTaxAmounts();
            if (cb.ID == "subpanelCompensationPenaltyCharge_cbIsWaived")
                item.RefreshAmounts(false, false, false, true);

        }
        else if (sender is UIFieldDropDownList)
        {
            UIFieldDropDownList ddl = (UIFieldDropDownList)sender;
            if (ddl.ID == "subpanelCompensationPenaltyCharge_ddlTaxCodeID")
                item.CalculateTaxAmounts();
        }

        subpanelCompensationPenaltyCharge.ObjectPanel.BindObjectToControls(item);
    }

    TaxInclusiveIndicator_CheckedChanged done
    IsWaived_CheckedChanged done
    TaxCodeID_SelectedIndexChanged done
    Quantity_TextChanged
    UnitPrice_TextChanged x 2


   protected void subpanelOneTimeCharge_DoCalculation_OnChanged(object sender, EventArgs e)
   {
       OLicenseOneTimeCharges charge = subpanelOneTimeCharge.SessionObject as OLicenseOneTimeCharges;
       subpanelOneTimeCharge.ObjectPanel.BindControlsToObject(charge);

       if (sender is UIFieldDropDownList)
       {
           UIFieldDropDownList ddl = (UIFieldDropDownList)sender;
           if (ddl.ID == "subpanelOneTimeCharge_ddlTaxCode")
               charge.CalculateAmounts();
       }

       else if (sender is UIFieldCheckBox)
       {
           UIFieldCheckBox cb = (UIFieldCheckBox)sender;
           if (cb.ID == "subpanelOneTimeCharge_cbIsWaived")
               charge.CalculateAmounts();
           else if (cb.ID == "subpanelOneTimeCharge_cbTaxInclusiveIndicator")
               charge.CalculateTaxAmounts();
       }
       else if (sender is UIFieldTextBox)
       {
           UIFieldTextBox tb = (UIFieldTextBox)sender;
           if (tb.ID == "subpanelOneTimeCharge_tbQuantity")
               charge.CalculateAmounts();
           else if (tb.ID == "subpanelOneTimeCharge_tbUnitPrice")
               charge.CalculateAmounts();
           else if (tb.ID == "subpanelOneTimeCharge_tbWaivedAmount")
               charge.CalculateAmounts();
       }

       subpanelOneTimeCharge.ObjectPanel.BindObjectToControls(charge);
   }


   public Dictionary<string, List<string>> DoValidationByCode(Guid? businessEntityID, Guid? businessUnitID, Guid? costCentreID, Guid? activityID, string code, out string consolidateErrorMessage, DataTable dt = null)
{
    List<string> messages = new List<string>();
    Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
    if (code == LicenseDepositsValidationItem.ChargeTypeEmpty)
    {
        if (ARChargeType == null || ARChargeType.IsDeleted == 1)
        {
            messages.Add(Resources.Errors.CPM_Validation_ChargeTypeEmpty);//"Charge type is empty or deleted.";
            result.Add(nameof(ARChargeTypeID), messages);
        }
    }
    else if (code == LicenseDepositsValidationItem.ChargeTypeNotForDeposit)
    {
        if (ARChargeType != null && ARChargeType?.DepositIndicator != 1)
        {
            messages.Add(Resources.Errors.CPM_Validation_ChargeTypeNotForDeposit);//"Charge type is not for deposit.";
            result.Add(nameof(ARChargeTypeID), messages);
        }
    }
    else if (code == LicenseDepositsValidationItem.TaxCodeEmpty)
    {
        if (TaxCode == null || TaxCode.IsDeleted == 1)
        {
            messages.Add(Resources.Errors.CPM_Validation_TaxCodeEmpty);//"Tax Code is empty or deleted.";
            result.Add(nameof(TaxCodeID), messages);
        }
    }
    else if (code == LicenseDepositsValidationItem.ChargeTypeAccessControl)
    {
        if (ARChargeType != null && businessEntityID != null && ARChargeType?.BusinessEntityID != null && ARChargeType?.BusinessEntityID != businessEntityID)
        {
            messages.Add(string.Format(Resources.Errors.General_ChargeTypeNotBelongToBE, ARChargeType?.ObjectName));
            result.Add(nameof(ARChargeTypeID), messages);
        }
        else if (ARChargeType != null && costCentreID != null && ARChargeType?.CostCentreID != null && ARChargeType.CostCentreID != costCentreID)
        {
            messages.Add(string.Format(Resources.Errors.General_ChargeTypeNotBelongToCC, ARChargeType?.ObjectName));
            result.Add(nameof(ARChargeTypeID), messages);
        }
        else if (ARChargeType != null && businessUnitID != null && ARChargeType?.BusinessUnitID != null && ARChargeType?.BusinessUnitID != businessUnitID)
        {
            messages.Add(string.Format(Resources.Errors.General_ChargeTypeNotBelongToBU, ARChargeType?.ObjectName));
            result.Add(nameof(ARChargeTypeID), messages);
        }
    }
    else if (code == LicenseDepositsValidationItem.TaxRateNotZero)
    {
        if (TaxCode != null && TaxCode.CurrentTaxRate != 0)
        {
            messages.Add(Resources.Errors.CPM_Validation_TaxRateNotZero);// "Current Tax Rate is not 0.";
            result.Add(nameof(TaxCodeID), messages);
        }
    }
    else if (code == LicenseDepositsValidationItem.GLAccountEmpty)
    {
        string glNumber = "";
        OGLAccount revenueAccount = GeneralChargeItem.GetGLAccount(ARChargeType, businessEntityID, businessUnitID, costCentreID, activityID, out glNumber);
        if (revenueAccount == null)
        {
            messages.Add(string.Format(Resources.Errors.General_CannotFindGLAccount_WithDes, glNumber, Description));
            result.Add(nameof(ARChargeTypeID), messages);
        }
    }
    consolidateErrorMessage = string.Join(GeneralValidationMessageTable.DefaultJoinText, messages);
    if (dt != null)
        GeneralValidationMessageTable.FillTable(dt, code, consolidateErrorMessage);
    return result;
}