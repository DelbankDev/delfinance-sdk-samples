package com.example.delfinance.samples;

import com.delfinance.qrcode.dtos.PayerDto;
import com.delfinance.qrcode.enums.QrCodeResponseFormat;
import com.delfinance.qrcode.requests.CreateDynamicDueDateQrCodeRequest;
import com.delfinance.qrcode.requests.CreateDynamicImmediateQrCodeRequest;
import com.delfinance.qrcode.requests.CreateStaticQrCodeRequest;
import com.delfinance.qrcode.requests.SearchStaticQrCodePaymentRequest;
import com.delfinance.qrcode.services.QrCodesService;

import java.math.BigDecimal;
import java.time.LocalDate;

import static com.example.delfinance.samples.Common.*;

public class QrCodeMethods {

    public static void sampleQrCode(QrCodesService service) {
        String pixKey = requiredEnv("PIX_KEY");
        BigDecimal qrAmount = new BigDecimal(optionalEnv("QRCODE_AMOUNT", "20"));
        QrCodeResponseFormat formatResponse = QrCodeResponseFormat.valueOf(
                optionalEnv("QRCODE_FORMAT_RESPONSE", "ONLY_PAYLOAD"));

        // Static QR Code
        CreateStaticQrCodeRequest staticRequest = new CreateStaticQrCodeRequest.Builder()
                .correlationId("qr-static-001")
                .amount(qrAmount)
                .pixKey(pixKey)
                .beneficiaryName("Empresa Exemplo")
                .additionalInfo(optionalEnv("QRCODE_ADDITIONAL_VALUE", "Pedido 123"))
                .build();
        System.out.println("[RUN] createStaticQrCode");
        logResult("createStaticQrCode", service.createStaticQrCode(staticRequest));

        System.out.println("[RUN] getStaticQrCodeById");
        logResult("getStaticQrCodeById", service.getStaticQrCodeById("qr-static-identifier"));

        SearchStaticQrCodePaymentRequest searchRequest = new SearchStaticQrCodePaymentRequest.Builder()
                .page("1")
                .size("20")
                .build();
        System.out.println("[RUN] getPaymentsStaticQrCodeById");
        logResult("getPaymentsStaticQrCodeById", service.getPaymentsStaticQrCodeById("qr-static-identifier", searchRequest));

        System.out.println("[RUN] cancelStaticQrCode");
        service.cancelStaticQrCode("qr-static-identifier");
        logResult("cancelStaticQrCode", "OK");

        // Dynamic Immediate QR Code
        CreateDynamicImmediateQrCodeRequest immediateRequest = new CreateDynamicImmediateQrCodeRequest.Builder()
                .correlationId("qr-immediate-001")
                .amount(qrAmount)
                .pixKey(pixKey)
                .expiresIn(optionalEnv("QRCODE_EXPIRES_IN", "600"))
                .formatResponse(formatResponse)
                .build();
        System.out.println("[RUN] createDynamicImmediateQrCode");
        logResult("createDynamicImmediateQrCode", service.createDynamicImmediateQrCode(immediateRequest));

        System.out.println("[RUN] getDynamicImmediateQrCodeById");
        logResult("getDynamicImmediateQrCodeById", service.getDynamicImmediateQrCodeById("qr-immediate-id"));

        System.out.println("[RUN] cancelDynamicImmediateQrCode");
        service.cancelDynamicImmediateQrCode("qr-immediate-id");
        logResult("cancelDynamicImmediateQrCode", "OK");

        // Dynamic Due Date QR Code
        PayerDto payer = new PayerDto.Builder()
                .name(optionalEnv("BENEFICIARY_HOLDER_NAME", "Cliente Exemplo"))
                .document(optionalEnv("BENEFICIARY_HOLDER_DOCUMENT", "12345678909"))
                .build();
        CreateDynamicDueDateQrCodeRequest dueDateRequest = new CreateDynamicDueDateQrCodeRequest.Builder()
                .correlationId("qr-due-date-001")
                .amount(qrAmount)
                .pixKey(pixKey)
                .dueDate(LocalDate.parse(optionalEnv("QRCODE_DUE_DATE", "2026-12-31")))
                .payer(payer)
                .formatResponse(formatResponse)
                .build();
        System.out.println("[RUN] createDynamicDueDateQrCode");
        logResult("createDynamicDueDateQrCode", service.createDynamicDueDateQrCode(dueDateRequest));

        System.out.println("[RUN] getDynamicDueDateQrCodeById");
        logResult("getDynamicDueDateQrCodeById", service.getDynamicDueDateQrCodeById("qr-due-date-id"));

        System.out.println("[RUN] cancelDynamicDueDateQrCode");
        service.cancelDynamicDueDateQrCode("qr-due-date-id");
        logResult("cancelDynamicDueDateQrCode", "OK");
    }

    public static void main(String[] args) {
        var client = buildClient();
        sampleQrCode(new QrCodesService(client));
    }
}
