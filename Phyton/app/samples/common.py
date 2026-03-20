import json
import os

from dotenv import load_dotenv

from delfinance.abstractions.enums.environment import Environment
from delfinance.abstractions.startup.delfinance_client import DelfinanceClient


load_dotenv()


def required_env(name: str) -> str:
    value = os.getenv(name)
    if not value:
        raise ValueError(f"Variavel obrigatoria ausente no .env: {name}")
    return value


def optional_env(name: str, default: str) -> str:
    return os.getenv(name, default)


def build_client() -> DelfinanceClient:
    environment_name = optional_env("SDK_ENVIRONMENT", "SANDBOX").upper()
    environment = Environment[environment_name]

    config = {
        "apiKey": required_env("AUTH_ACCOUNT_API_KEY"),
        "accountId": required_env("AUTH_ACCOUNT_ID"),
        "environment": environment,
    }

    certificate_path = os.getenv("CERTIFICATE_PATH")
    private_key_path = os.getenv("PRIVATE_KEY_PATH")
    if certificate_path:
        config["certificatePath"] = certificate_path
    if private_key_path:
        config["privateKeyPath"] = private_key_path

    return DelfinanceClient(config)


def _to_serializable(value):
    if value is None:
        return None
    if isinstance(value, (str, int, float, bool)):
        return value
    if isinstance(value, dict):
        return {k: _to_serializable(v) for k, v in value.items()}
    if isinstance(value, (list, tuple, set)):
        return [_to_serializable(v) for v in value]
    if hasattr(value, "__dict__"):
        return {k: _to_serializable(v) for k, v in vars(value).items()}
    return str(value)


def log_result(label: str, result) -> None:
    payload = _to_serializable(result)
    print(f"[RESULT] {label}")
    print(json.dumps(payload, ensure_ascii=False, indent=2, default=str))
