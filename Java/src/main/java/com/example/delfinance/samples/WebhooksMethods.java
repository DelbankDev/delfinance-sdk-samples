package com.example.delfinance.samples;

import com.delfinance.webhooks.enums.AuthorizationScheme;
import com.delfinance.webhooks.enums.EventType;
import com.delfinance.webhooks.requests.WebhookRequest;
import com.delfinance.webhooks.services.WebhooksService;

import static com.example.delfinance.samples.Common.*;

public class WebhooksMethods {

    public static void sampleWebhooks(WebhooksService service) {
        WebhookRequest webhookRequest = new WebhookRequest.Builder()
                .eventType(EventType.PIX_RECEIVED)
                .url("https://sua-api.com/webhooks/delfinance")
                .authorizationScheme(AuthorizationScheme.BEARER)
                .authorization("SEU_TOKEN")
                .build();

        //System.out.println("[RUN] createWebhook");
       // logResult("createWebhook", service.createWebhook(webhookRequest));

        System.out.println("[RUN] getAllWebhooks");
        logResult("getAllWebhooks", service.getAllWebhooks());

        String webhookId = optionalEnv("WEBHOOK_ID", "webhook-id-001");

        System.out.println("[RUN] getWebhookById");
        logResult("getWebhookById", service.getWebhookById(webhookId));

        System.out.println("[RUN] updateWebhook");
        logResult("updateWebhook", service.updateWebhook(webhookRequest, webhookId));

        System.out.println("[RUN] deleteWebhook");
        service.deleteWebhook(webhookId);
        logResult("deleteWebhook", "OK");
    }

    public static void main(String[] args) {
        var client = buildClient();
        sampleWebhooks(new WebhooksService(client));
    }
}
