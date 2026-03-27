import argparse
import sys
import uuid
from pathlib import Path

from delfinance.transfers.dto.beneficiary import Beneficiary, BeneficiaryHolder
from delfinance.transfers.requests.create_ted_transfer_request import CreateTedTransferRequest
from delfinance.transfers.requests.create_transfer_request import CreateTransferRequest
from delfinance.transfers.services.transfers_service import TransfersService

try:
    from app.samples.common import build_client, log_result, optional_env
except ModuleNotFoundError:
    sys.path.append(str(Path(__file__).resolve().parents[2]))
    from app.samples.common import build_client, log_result, optional_env



def sample_transfers(service: TransfersService) -> None:
    print("[RUN] get_transfer")
    transfer = service.get_transfer(optional_env("TRANSFER_ID", "2a2f3057-9974-4b05-8069-fb69c362c369"))
    log_result("get_transfer", transfer)

    transfer_request = CreateTransferRequest(
        amount=float(optional_env("AMOUNT", "100")),
        description="Pagamento Pix de teste",
        end_to_end_id=optional_env("END_TO_END_ID", "E12345678901234567890123456789012"),
        beneficiary_account=optional_env("BENEFICIARY_ACCOUNT", "42790"),
    )
    print("[RUN] create_transfer")
    transfer = service.create_transfer(transfer_request, idempotency_key=str(uuid.uuid4()))
    log_result("create_transfer", transfer)

    beneficiary = Beneficiary(
        participant_ispb=optional_env("ISPB", "12345678"),
        branch=optional_env("BENEFICIARY_BRANCH", "0001"),
        number=optional_env("BENEFICIARY_ACCOUNT", "123456"),
        holder=BeneficiaryHolder(
            name=optional_env("BENEFICIARY_HOLDER_NAME", "Fornecedor Exemplo"),
            document=optional_env("BENEFICIARY_HOLDER_DOCUMENT", "12345678909"),
        ),
        account_type="CURRENT",
    )
    ted_request = CreateTedTransferRequest(
        amount=float(optional_env("AMOUNT", "250")),
        beneficiary=beneficiary,
        description="Pagamento TED de teste",
    )
    print("[RUN] create_ted_transfer")
    ted_transfer = service.create_ted_transfer(ted_request, idempotency_key=str(uuid.uuid4()))
    log_result("create_ted_transfer", ted_transfer)


def _parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Executa sample de Transfers")
    parser.add_argument("--transfer", action="store_true", help="Compatibilidade de chamada")
    return parser.parse_args()


if __name__ == "__main__":
    _parse_args()
    client = build_client()
    service = TransfersService(client)
    sample_transfers(service)
