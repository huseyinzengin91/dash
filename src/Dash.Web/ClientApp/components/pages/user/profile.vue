<template>
  <div>
    <content-placeholders v-if="!isLoaded">
      <content-placeholders-heading :img="true"/>
      <content-placeholders-text :lines="3"/>
    </content-placeholders>

    <div v-if="isLoaded">
      <h1>My Profile</h1>
      <form>
        <div class="form-group row">
          <label for="firstname" class="col-sm-2 col-form-label">Firstname</label>
          <div class="col-sm-10">
            <input
              type="text"
              v-model="model.firstname"
              class="form-control"
              id="firstname"
              placeholder="Firstname"
            >
          </div>
        </div>
        <div class="form-group row">
          <label for="lastname" class="col-sm-2 col-form-label">Lastname</label>
          <div class="col-sm-10">
            <textarea
              class="form-control"
              v-model="model.lastname"
              id="lastname"
              placeholder="Lastname"
            ></textarea>
          </div>
        </div>
        <div class="form-group row">
          <label for="email" class="col-sm-2 col-form-label">Email</label>
          <div class="col-sm-10">
            <input
              type="email"
              v-model="model.email"
              class="form-control"
              id="email"
              placeholder="Email"
            >
          </div>
        </div>
        <div class="form-group row">
          <label for="username" class="col-sm-2 col-form-label">Username</label>
          <div class="col-sm-10">
            <input
              type="text"
              v-model="model.username"
              class="form-control"
              id="username"
              placeholder="Username"
            >
          </div>
        </div>
        <div class="form-group row">
          <label for="password" class="col-sm-2 col-form-label">Password</label>
          <div class="col-sm-10">
            <input
              type="password"
              v-model="model.password"
              class="form-control"
              id="password"
              placeholder="Password"
            >
          </div>
        </div>
        <div class="form-group row">
          <div class="col-sm-10">
            <button type="button" @click="saveChanges" class="btn btn-primary">Save</button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "profile",
  data() {
    return {
      isLoaded: false,
      model: {
        firstname: null,
        lastname: null,
        email: null,
        username: null,
        password: null
      }
    };
  },
  mounted() {
    this.getProfile();
  },
  methods: {
    getProfile() {
      this.$http
        .get("/api/v1/User/Get")
        .then(response => {
          if (response.data.success) {
            this.model.firstname = response.data.data.firstname;
            this.model.lastname = response.data.data.lastname;
            this.model.email = response.data.data.email;
            this.model.username = response.data.data.username;
          }

          this.isLoaded = true;
        })
        .catch(err => {
          this.$notify({
            type: "error",
            title: "Error",
            text: err.response.data.message
          });
          console.error(err.response);
        });
    },
    saveChanges() {
      this.$http
        .post("/api/v1/User/Save", this.model)
        .then(response => {
          if (response.data.success) {
            this.$notify({
              type: "success",
              title: "Success",
              text: response.data.message
            });
          }
        })
        .catch(err => {
          this.$notify({
            type: "error",
            title: "Error",
            text: err.response.data.message
          });
          console.error(err.response);
        });
    }
  }
};
</script>

<style>
</style>
