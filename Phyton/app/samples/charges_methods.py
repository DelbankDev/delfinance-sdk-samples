import argparse
import sys
from pathlib import Path

from delfinance.charges.requests.bill_payments_request import BillPaymentsRequest
from delfinance.charges.requests.create_charge_request import CreateChargeRequest
from delfinance.charges.requests.list_charges_by_period_request import ListChargesByPeriodRequest
from delfinance.charges.services.charges_service import ChargesService

try:
    from app.samples.common import build_client, log_result, optional_env, required_env
except ModuleNotFoundError:
    sys.path.append(str(Path(__file__).resolve().parents[2]))
    from app.samples.common import build_client, log_result, optional_env, required_env


def sample_charges(service: ChargesService) -> None:
    pix_key = required_env("PIX_KEY")

    create_charge_request = CreateChargeRequest(
        type="BANKSLIP",
        correlation_id="charge-correlation-001",
        your_number=optional_env("CHARGE_YOUR_NUMBER", "65481"),
        amount=70.0,
        due_date="2026-03-31",
        payer={
            "name": optional_env("BENEFICIARY_HOLDER_NAME", "Cliente Exemplo"),
            "document": optional_env("BENEFICIARY_HOLDER_DOCUMENT", "12345678909"),
            "email": "cliente@example.com",
            "address": {
                "street": optional_env("QRCODE_STREET", "Rua Exemplo"),
                "number": optional_env("QRCODE_NUMBER", "123"),
                "city": optional_env("QRCODE_CITY_NAME", "Sao Paulo"),
                "state": optional_env("QRCODE_UF", "SP"),
                "zipCode": optional_env("QRCODE_ZIP_CODE", "01001000"),
                "publicPlace": optional_env("QRCODE_PUBLIC_PLACE", "AVENIDA"),
                "neighborhood": optional_env("QRCODE_NEIGHBORHOOD", "Bairro Exemplo"),
                "complement": optional_env("QRCODE_COMPLEMENT", ""),
            },
        }
    )

    print("[RUN] create_charge")
    created = service.create_charge(create_charge_request)
    log_result("create_charge", created)

    print("[RUN] get_charge_by_id")
    charge = service.get_charge_by_id("charge-correlation-001")
    log_result("get_charge_by_id", charge)

    list_request = ListChargesByPeriodRequest(
        page=1,
        limit=20,
        start_date="2026-01-01",
        end_date="2026-12-31",
    )
    print("[RUN] list_charges_by_period")
    charges = service.list_charges_by_period(list_request)
    log_result("list_charges_by_period", charges)

    print("[RUN] download_charge_pdf")
    pdf = service.download_charge_pdf("charge-id-001")
    print(f"[OK] download_charge_pdf: bytes={len(pdf.getvalue())}")

    print("[RUN] update_charge")
    updated = service.update_charge("charge-correlation-001", create_charge_request)
    log_result("update_charge", updated)

    print("[RUN] void_charge")
    voided = service.void_charge("charge-correlation-001")
    log_result("void_charge", voided)

    print("[RUN] validate_payment_details")
    details = service.validate_payment_details("00190500954014481606906809350314337370000000100")
    log_result("validate_payment_details", details)

    bill_payment_request = BillPaymentsRequest(
        digitable_line="00190500954014481606906809350314337370000000100",
        amount=99.90,
        pay_at="2026-04-15",
        category="UTILITY",
    )
    print("[RUN] bill_payment")
    payment = service.bill_payment(bill_payment_request, idempotency_key="idem-bill-001")
    log_result("bill_payment", payment)


def _parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Executa sample de Charges")
    parser.add_argument("--charge", action="store_true", help="Compatibilidade de chamada")
    return parser.parse_args()


if __name__ == "__main__":
    _parse_args()
    client = build_client()
    service = ChargesService(client)
    sample_charges(service)
