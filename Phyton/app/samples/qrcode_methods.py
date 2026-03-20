import argparse
import sys
from pathlib import Path

from delfinance.qrcode.requests.due_date_qr_code_request import DueDateQrCodeRequest
from delfinance.qrcode.requests.immediate_qr_code_request import ImmediateQrCodeRequest
from delfinance.qrcode.requests.static_qr_code_request import StaticQrCodeRequest
from delfinance.qrcode.services.qr_code_service import QrCodeService

try:
    from app.samples.common import build_client, log_result, optional_env, required_env
except ModuleNotFoundError:
    sys.path.append(str(Path(__file__).resolve().parents[2]))
    from app.samples.common import build_client, log_result, optional_env, required_env


def sample_qr_code(service: QrCodeService) -> None:
    pix_key = required_env("PIX_KEY")
    qr_amount = float(optional_env("QRCODE_AMOUNT", "20"))

    static_request = StaticQrCodeRequest(
        correlation_id="qr-static-001",
        amount=qr_amount,
        pix_key=pix_key,
        beneficiary_name="Empresa Exemplo",
        additional_info=optional_env("QRCODE_ADDITIONAL_VALUE", "Pedido 123"),
        source=optional_env("QRCODE_SOURCE", "DEFAULT"),
        format_response=optional_env("QRCODE_FORMAT_RESPONSE", "ONLY_PAYLOAD"),
    )
    print("[RUN] create_static_qr_code")
    static_created = service.create_static_qr_code(static_request)
    log_result("create_static_qr_code", static_created)

    print("[RUN] get_static_qr_code")
    static_qr = service.get_static_qr_code("qr-static-identifier")
    log_result("get_static_qr_code", static_qr)

    print("[RUN] get_static_qr_code_payments")
    static_payments = service.get_static_qr_code_payments("qr-static-identifier")
    log_result("get_static_qr_code_payments", static_payments)

    print("[RUN] cancel_static_qr_code")
    static_cancel = service.cancel_static_qr_code("qr-static-identifier")
    log_result("cancel_static_qr_code", static_cancel)

    immediate_request = ImmediateQrCodeRequest(
        correlation_id="qr-immediate-001",
        amount=qr_amount,
        pix_key=pix_key,
        expires_in=int(optional_env("QRCODE_EXPIRES_IN", "600")),
        format_response=optional_env("QRCODE_FORMAT_RESPONSE", "ONLY_PAYLOAD"),
    )
    print("[RUN] create_immediate_qr_code")
    immediate_created = service.create_immediate_qr_code(immediate_request)
    log_result("create_immediate_qr_code", immediate_created)

    print("[RUN] get_immediate_qr_code")
    immediate_qr = service.get_immediate_qr_code("qr-immediate-id")
    log_result("get_immediate_qr_code", immediate_qr)

    print("[RUN] cancel_immediate_qr_code")
    immediate_cancel = service.cancel_immediate_qr_code("qr-immediate-id")
    log_result("cancel_immediate_qr_code", immediate_cancel)

    due_date_request = DueDateQrCodeRequest(
        correlation_id="qr-due-date-001",
        amount=qr_amount,
        pix_key=pix_key,
        due_date=optional_env("QRCODE_DUE_DATE", "2026-12-31"),
        payer={
            "name": optional_env("BENEFICIARY_HOLDER_NAME", "Cliente Exemplo"),
            "document": optional_env("BENEFICIARY_HOLDER_DOCUMENT", "12345678909"),
        },
        format_response=optional_env("QRCODE_FORMAT_RESPONSE", "ONLY_PAYLOAD"),
    )
    print("[RUN] create_due_date_qr_code")
    due_date_created = service.create_due_date_qr_code(due_date_request)
    log_result("create_due_date_qr_code", due_date_created)

    print("[RUN] get_due_date_qr_code")
    due_date_qr = service.get_due_date_qr_code("qr-due-date-id")
    log_result("get_due_date_qr_code", due_date_qr)

    print("[RUN] cancel_due_date_qr_code")
    due_date_cancel = service.cancel_due_date_qr_code("qr-due-date-id")
    log_result("cancel_due_date_qr_code", due_date_cancel)


def _parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Executa sample de QRCode")
    parser.add_argument("--qrcode", action="store_true", help="Compatibilidade de chamada")
    return parser.parse_args()


if __name__ == "__main__":
    _parse_args()
    client = build_client()
    service = QrCodeService(client)
    sample_qr_code(service)
