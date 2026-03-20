import argparse
import sys
from pathlib import Path

from delfinance.transfers.requests.create_pix_key_request import CreatePixKeyRequest
from delfinance.transfers.requests.decode_qr_code_request import DecodeQrCodeRequest
from delfinance.transfers.requests.delete_pix_key_request import DeletePixKeyRequest
from delfinance.transfers.requests.generate_auth_code_request import GenerateAuthCodeRequest
from delfinance.transfers.requests.payment_initialization_request import PaymentInitializationRequest
from delfinance.transfers.services.pix_service import PixService

try:
    from app.samples.common import build_client, log_result, optional_env, required_env
except ModuleNotFoundError:
    sys.path.append(str(Path(__file__).resolve().parents[2]))
    from app.samples.common import build_client, log_result, optional_env, required_env


def sample_pix(service: PixService) -> None:
    payment_init_request = PaymentInitializationRequest(
        key=required_env("PIX_KEY"),
        holder_document=optional_env("BENEFICIARY_HOLDER_DOCUMENT", "12345678909"),
    )
    print("[RUN] payment_initialization")
    payment_init = service.payment_initialization(payment_init_request)
    log_result("payment_initialization", payment_init)

    decode_request = DecodeQrCodeRequest(payload=required_env("PAYLOAD_QR_CODE"))
    print("[RUN] decode_qr_code")
    decoded = service.decode_qr_code(decode_request)
    log_result("decode_qr_code", decoded)

    create_key_request = CreatePixKeyRequest(entry_type="EVP")
    print("[RUN] create_pix_key")
    key_created = service.create_pix_key(create_key_request, idempotency_key="idem-pix-key-create-001")
    log_result("create_pix_key", key_created)

    print("[RUN] get_pix_keys")
    keys = service.get_pix_keys()
    log_result("get_pix_keys", keys)

    delete_key_request = DeletePixKeyRequest(entry_type="EVP", key=required_env("PIX_KEY"))
    print("[RUN] delete_pix_key")
    deleted = service.delete_pix_key(delete_key_request, idempotency_key="idem-pix-key-delete-001")
    log_result("delete_pix_key", deleted)

    auth_code_request = GenerateAuthCodeRequest(
        sender=optional_env("AUTH_CODE_SENDER", "EMAIL"),
        receiver=optional_env("AUTH_CODE_RECEIVER", "pix-chave@exemplo.com"),
        payload="Seu codigo de autenticacao e {{code}}",
    )
    print("[RUN] generate_auth_code")
    auth_code = service.generate_auth_code(auth_code_request)
    log_result("generate_auth_code", auth_code)


def _parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Executa sample de Pix")
    parser.add_argument("--pix", action="store_true", help="Compatibilidade de chamada")
    return parser.parse_args()


if __name__ == "__main__":
    _parse_args()
    client = build_client()
    service = PixService(client)
    sample_pix(service)
