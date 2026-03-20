package com.example.delfinance;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.assertNotNull;

class SdkDependencyTest {

    @Test
    void shouldLoadDelfinanceClientBuilderClass() throws Exception {
        Class<?> clazz = Class.forName("com.delfinance.abstractions.startup.DelfinanceClientBuilder");
        assertNotNull(clazz);
    }
}