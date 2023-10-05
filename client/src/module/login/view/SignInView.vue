<template>
  <v-layout align-center justify-center>
    <v-flex xs12 sm8 md4>
      <v-card tile elevation="2">
        <v-card-text>
          <v-form @submit.prevent="controller.signIn()">
            <v-text-field
              v-model="controller.form.email"
              label="Email"
              required
            ></v-text-field>
            <v-text-field
              v-model="controller.form.password"
              label="Password"
              :append-icon="controller.showPassword ? 'mdi-eye' : 'mdi-eye-off'"
              :type="controller.showPassword ? 'text' : 'password'"
              required
              @click:append="controller.togglePasswordVisibility()"
            ></v-text-field>
            <v-btn class="login-button" color="primary" type="submit"
              >Sign in</v-btn
            >
          </v-form>
        </v-card-text>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script lang="ts">
import { authenticateController } from "../../di/di";

export default {
  name: "SignInView",
  data: (context: any) => ({
    controller: authenticateController(context),
  }),
};
</script>

<style scoped>
.card-container {
  padding: 1rem;
  border-radius: 1rem;
  box-shadow: 0 0 0.625rem rgba(0, 0, 0, 0.2);
}

.login-button {
  width: 100%;
  /* margin-top: 1rem; */
}
</style>
