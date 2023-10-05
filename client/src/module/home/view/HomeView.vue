<template>
  <v-container>
    <v-row justify="center" align="center">
      <v-col cols="12" lg="12" sm="8" md="6">
        <h1 class="text-center">Welcome to CRM System</h1>
        <!-- <p class="text-center">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse
          eu justo a elit interdum pellentesque. Integer ut libero justo.
        </p> -->
        <v-spacer />

        <router-link to="/users/signin">
          <v-btn class="btn" color="primary">Sign In</v-btn>
        </router-link>

        <router-link to="/users/signup">
          <v-btn class="btn" color="secondary">Sign Up</v-btn>
        </router-link>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: "HomeView",
  // Add any logic you may need here
};
</script>

<style>
.btn {
  display: inline-block;
  margin-top: 0.5rem;
  width: 100%;
}
</style>
