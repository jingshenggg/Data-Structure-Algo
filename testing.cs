Public invoice GetOneInvoice (DateTime tenantStartDate, DateTime tenantEndDate, DateTime invoiceStartDate, double basicMonthlyFee) {
double fee = 0;
	if (tenantStartDate.Month == invoiceStartDate.Month && tenantStartDate.Year == invoiceStartDate.Year) {
		int daysInMonth = DateTime.DaysInMonth(invoiceStartDate.Year, invoiceStartDate.Month);
		fee = (daysInMonth – invoiceStartDate.Day + 1) / daysInMonth * basicMontlyFee;
	}  else if ( tenantEndDate.Month == invoiceStartDate.Month && tenantEndDate.Year == invoiceEndtDate.Year) {
		int daysInMonth = DateTime.DaysInMonth(invoiceStartDate.Year, invoiceStartDate.Month);
		fee = (1 – ((daysInMonth – invoiceStartDate.Day + 1) / daysInMonth)) * basicMonthlyFee;
	} else {
		fee = basicMonthlyFee;
	}

	return new Invoice(invoiceStartDate, fee);
}
