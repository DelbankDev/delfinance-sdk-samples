from delfinance.webhooks.requests.create_webhook_request import CreateWebhookRequest
from delfinance.webhooks.services.webhook_service import WebhookService

from app.samples.common import log_result, optional_env


def sample_webhooks(service: WebhookService) -> None:
    webhook_request = CreateWebhookRequest(
        event_type="PIX_RECEIVED",
        url="https://sua-api.com/webhooks/delfinance",
        authorization_scheme="BEARER",
        authorization="SEU_TOKEN",
    )

    print("[RUN] create_webhook")
    created = service.create_webhook(webhook_request)
    log_result("create_webhook", created)

    print("[RUN] get_all_webhooks")
    all_webhooks = service.get_all_webhooks()
    log_result("get_all_webhooks", all_webhooks)

    webhook_id = optional_env("WEBHOOK_ID", "webhook-id-001")

    print("[RUN] get_webhook_by_id")
    webhook = service.get_webhook_by_id(webhook_id)
    log_result("get_webhook_by_id", webhook)

    print("[RUN] update_webhook")
    updated = service.update_webhook(webhook_id, webhook_request)
    log_result("update_webhook", updated)

    print("[RUN] delete_webhook")
    deleted = service.delete_webhook(webhook_id)
    log_result("delete_webhook", deleted)
