package com.example.delfinance.samples;

import com.delfinance.transfers.dtos.BankAccountHolderDto;
import com.delfinance.transfers.dtos.TransferAccountRequestDto;
import com.delfinance.transfers.enums.BankAccountType;
import com.delfinance.transfers.requests.CreateTransferRequest;
import com.delfinance.transfers.services.TransfersService;

import java.math.BigDecimal;
import java.util.UUID;

import static com.example.delfinance.samples.Common.*;

public class TransfersMethods {

    public static void sampleTransfers(TransfersService service) {
        System.out.println("[RUN] getTransfer");
        logResult("getTransfer", service.getTransfer(optionalEnv("TRANSFER_ID", "2a2f3057-9974-4b05-8069-fb69c362c369")));

        // Internal / Pix transfer (via beneficiaryAccount)
        CreateTransferRequest transferRequest = new CreateTransferRequest.Builder()
                .amount(new BigDecimal(optionalEnv("AMOUNT", "100")))
                .description("Pagamento Pix de teste")
                .endToEndId(optionalEnv("END_TO_END_ID", "E12345678901234567890123456789012"))
                .beneficiaryAccount(optionalEnv("BENEFICIARY_ACCOUNT", "42790"))
                .build();
        System.out.println("[RUN] createTransfer");
        logResult("createTransfer", service.createTransfer(UUID.randomUUID().toString(), transferRequest, false));

        // TED transfer (via beneficiary with external account details)
        BankAccountHolderDto holder = new BankAccountHolderDto.Builder()
                .name(optionalEnv("BENEFICIARY_HOLDER_NAME", "Fornecedor Exemplo"))
                .document(optionalEnv("BENEFICIARY_HOLDER_DOCUMENT", "12345678909"))
                .build();
        TransferAccountRequestDto beneficiary = new TransferAccountRequestDto.Builder()
                .participantIspb(optionalEnv("ISPB", "12345678"))
                .branch(optionalEnv("BENEFICIARY_BRANCH", "0001"))
                .number(optionalEnv("BENEFICIARY_ACCOUNT", "123456"))
                .holder(holder)
                .type(BankAccountType.CURRENT)
                .build();
        CreateTransferRequest tedRequest = new CreateTransferRequest.Builder()
                .amount(new BigDecimal(optionalEnv("AMOUNT", "250")))
                .beneficiary(beneficiary)
                .description("Pagamento TED de teste")
                .build();
        System.out.println("[RUN] createTransfer (TED)");
        logResult("createTransfer (TED)", service.createTransfer(UUID.randomUUID().toString(), tedRequest, true));
    }

    public static void main(String[] args) {
        var client = buildClient();
        sampleTransfers(new TransfersService(client));
    }
}
