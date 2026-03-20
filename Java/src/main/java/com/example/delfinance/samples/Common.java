package com.example.delfinance.samples;

import com.delfinance.abstractions.enums.Environment;
import com.delfinance.abstractions.startup.DelfinanceClient;
import com.delfinance.abstractions.startup.DelfinanceClientBuilder;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import io.github.cdimascio.dotenv.Dotenv;

public class Common {

    private static final Dotenv dotenv = Dotenv.configure().ignoreIfMissing().load();
    private static final Gson gson = new GsonBuilder().setPrettyPrinting().create();

    public static String requiredEnv(String name) {
        String value = dotenv.get(name, System.getenv(name));
        if (value == null || value.isBlank()) {
            throw new IllegalStateException("Variavel obrigatoria ausente no .env: " + name);
        }
        return value;
    }

    public static String optionalEnv(String name, String defaultValue) {
        String value = dotenv.get(name, System.getenv(name));
        return (value != null && !value.isBlank()) ? value : defaultValue;
    }

    public static DelfinanceClient buildClient() {
        String environmentName = optionalEnv("SDK_ENVIRONMENT", "SANDBOX").toUpperCase();
        Environment environment = Environment.valueOf(environmentName);

        DelfinanceClientBuilder builder = new DelfinanceClientBuilder()
                .accountId(requiredEnv("AUTH_ACCOUNT_ID"))
                .apiKey(requiredEnv("AUTH_ACCOUNT_API_KEY"))
                .environment(environment);

        String certPath = dotenv.get("CERTIFICATE_PATH", System.getenv("CERTIFICATE_PATH"));
        String privateKeyPath = dotenv.get("PRIVATE_KEY_PATH", System.getenv("PRIVATE_KEY_PATH"));
        if (certPath != null && !certPath.isBlank()) {
            builder.certificatePath(certPath);
        }
        if (privateKeyPath != null && !privateKeyPath.isBlank()) {
            builder.privateKeyPath(privateKeyPath);
        }

        return builder.build();
    }

    public static void logResult(String label, Object result) {
        System.out.println("[RESULT] " + label);
        System.out.println(gson.toJson(result));
    }
}
