package com.example.delfinance.samples;

import com.delfinance.charges.dtos.ChargePayerAddressDto;
import com.delfinance.charges.dtos.ChargePayerDto;
import com.delfinance.charges.enums.ChargeType;
import com.delfinance.charges.enums.TransactionCategory;
import com.delfinance.charges.requests.BillPaymentsRequest;
import com.delfinance.charges.requests.CreateChargeRequest;
import com.delfinance.charges.requests.ListChargesByPeriodRequest;
import com.delfinance.charges.services.ChargersService;

import java.math.BigDecimal;
import java.time.OffsetDateTime;

import static com.example.delfinance.samples.Common.*;

public class ChargesMethods {

    public static void sampleCharges(ChargersService service) {
        ChargePayerAddressDto address = new ChargePayerAddressDto.Builder()
                .publicPlace(optionalEnv("QRCODE_PUBLIC_PLACE", "AVENIDA"))
                .number(optionalEnv("QRCODE_NUMBER", "123"))
                .neighborhood(optionalEnv("QRCODE_NEIGHBORHOOD", "Bairro Exemplo"))
                .city(optionalEnv("QRCODE_CITY_NAME", "Sao Paulo"))
                .state(optionalEnv("QRCODE_UF", "SP"))
                .zipCode(optionalEnv("QRCODE_ZIP_CODE", "01001000"))
                .complement(optionalEnv("QRCODE_COMPLEMENT", ""))
                .build();

        ChargePayerDto payer = new ChargePayerDto.Builder()
                .name(optionalEnv("BENEFICIARY_HOLDER_NAME", "Cliente Exemplo"))
                .document(optionalEnv("BENEFICIARY_HOLDER_DOCUMENT", "12345678909"))
                .email("cliente@example.com")
                .address(address)
                .build();

        CreateChargeRequest createChargeRequest = new CreateChargeRequest.Builder()
                .type(ChargeType.BANKSLIP)
                .correlationId("charge-correlation-001")
                .yourNumber(optionalEnv("CHARGE_YOUR_NUMBER", "65481"))
                .amount(new BigDecimal("70.00"))
                .dueDate(OffsetDateTime.parse("2026-03-31T00:00:00Z"))
                .payer(payer)
                .build();

        System.out.println("[RUN] createCharge");
        logResult("createCharge", service.createCharge(createChargeRequest));

        System.out.println("[RUN] getChargeById");
        logResult("getChargeById", service.getChargeById("charge-correlation-001"));

        ListChargesByPeriodRequest listRequest = new ListChargesByPeriodRequest.Builder()
                .page(1)
                .limit(20)
                .startDate(OffsetDateTime.parse("2026-01-01T00:00:00Z"))
                .endDate(OffsetDateTime.parse("2026-12-31T23:59:59Z"))
                .build();
        System.out.println("[RUN] listChargesByPeriod");
        logResult("listChargesByPeriod", service.listChargesByPeriod(listRequest));

        System.out.println("[RUN] downloadChargePdf");
        var pdf = service.downloadChargePdf("charge-id-001");
        System.out.println("[OK] downloadChargePdf: " + pdf);

        System.out.println("[RUN] updateCharge");
        service.updateCharge("charge-correlation-001", createChargeRequest);
        logResult("updateCharge", "OK");

        System.out.println("[RUN] voidCharge");
        service.voidCharge("charge-correlation-001");
        logResult("voidCharge", "OK");

        System.out.println("[RUN] validatePaymentDetails");
        logResult("validatePaymentDetails", service.validatePaymentDetails("00190500954014481606906809350314337370000000100"));

        BillPaymentsRequest billPaymentRequest = new BillPaymentsRequest.Builder()
                .digitableLine("00190500954014481606906809350314337370000000100")
                .amount(new BigDecimal("99.90"))
                .payAt(OffsetDateTime.parse("2026-04-15T00:00:00Z"))
                .category(TransactionCategory.OTHERS)
                .build();
        System.out.println("[RUN] billPayment");
        logResult("billPayment", service.billPayment(billPaymentRequest, "idem-bill-001"));
    }

    public static void main(String[] args) {
        var client = buildClient();
        sampleCharges(new ChargersService(client));
    }
}
