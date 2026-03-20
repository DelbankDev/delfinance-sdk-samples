package com.example.delfinance.samples;

import com.delfinance.abstractions.startup.DelfinanceClient;
import com.delfinance.payments.initialization.requests.DecodeQrCodeRequest;
import com.delfinance.payments.initialization.requests.PixKeyInitializationRequest;
import com.delfinance.payments.initialization.services.DecodeQrCodeService;
import com.delfinance.payments.initialization.services.PixKeyInitializationService;
import com.delfinance.pixkeys.enums.CodeSender;
import com.delfinance.pixkeys.enums.KeyType;
import com.delfinance.pixkeys.requests.AuthCodeRequest;
import com.delfinance.pixkeys.requests.CreatePixKeyRequest;
import com.delfinance.pixkeys.requests.DeletePixKeyRequest;
import com.delfinance.pixkeys.services.PixKeysService;

import static com.example.delfinance.samples.Common.*;

public class PixMethods {

    public static void samplePix(DelfinanceClient client) {
        // Payment initialization
        PixKeyInitializationService initService = new PixKeyInitializationService(client);
        PixKeyInitializationRequest initRequest = new PixKeyInitializationRequest.Builder()
                .key(requiredEnv("PIX_KEY"))
                .holderDocument(optionalEnv("BENEFICIARY_HOLDER_DOCUMENT", "12345678909"))
                .build();
        System.out.println("[RUN] pixKeyInitialization");
        logResult("pixKeyInitialization", initService.pixKeyInitialization(initRequest));

        // Decode QR Code
        DecodeQrCodeService decodeService = new DecodeQrCodeService(client);
        DecodeQrCodeRequest decodeRequest = new DecodeQrCodeRequest(requiredEnv("PAYLOAD_QR_CODE"));
        System.out.println("[RUN] decodeQrCode");
        logResult("decodeQrCode", decodeService.decodeQrCode(decodeRequest));

        // Pix Keys
        PixKeysService pixKeysService = new PixKeysService(client);

        CreatePixKeyRequest createKeyRequest = new CreatePixKeyRequest.Builder()
                .entryType(KeyType.EVP)
                .build();
        System.out.println("[RUN] createPixKey");
        logResult("createPixKey", pixKeysService.createPixKey(createKeyRequest, "idem-pix-key-create-001"));

        System.out.println("[RUN] getAllKeys");
        logResult("getAllKeys", pixKeysService.getAllKeys());

        DeletePixKeyRequest deleteKeyRequest = new DeletePixKeyRequest.Builder()
                .entryType(KeyType.EVP)
                .key(requiredEnv("PIX_KEY"))
                .build();
        System.out.println("[RUN] deleteKey");
        pixKeysService.deleteKey(deleteKeyRequest, "idem-pix-key-delete-001");
        logResult("deleteKey", "OK");

        AuthCodeRequest authCodeRequest = new AuthCodeRequest.Builder()
                .sender(CodeSender.EMAIL)
                .receiver(optionalEnv("AUTH_CODE_RECEIVER", "pix-chave@exemplo.com"))
                .payload("Seu codigo de autenticacao e {{code}}")
                .build();
        System.out.println("[RUN] createAuthenticationCode");
        logResult("createAuthenticationCode", pixKeysService.createAuthenticationCode(authCodeRequest));
    }

    public static void main(String[] args) {
        var client = buildClient();
        samplePix(client);
    }
}
