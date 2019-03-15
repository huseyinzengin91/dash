<template>
  <div>
    <h1>Edit "{{model.name}}" Site</h1>

    <form>
      <div class="form-group row">
        <label for="name" class="col-sm-2 col-form-label">Name</label>
        <div class="col-sm-10">
          <input type="text" v-model="model.name" class="form-control" id="name" placeholder="Name">
        </div>
      </div>
      <div class="form-group row">
        <label for="description" class="col-sm-2 col-form-label">Description</label>
        <div class="col-sm-10">
          <textarea
            class="form-control"
            v-model="model.description"
            id="description"
            placeholder="Description"
          ></textarea>
        </div>
      </div>
      <div class="form-group row">
        <label for="siteAddress" class="col-sm-2 col-form-label">Site Address</label>
        <div class="col-sm-10">
          <input
            type="text"
            v-model="model.siteAddress"
            class="form-control"
            id="siteAddress"
            placeholder="Site Address (www.google.com)"
          >
        </div>
      </div>
      <div class="form-group row">
        <label for="description" class="col-sm-2 col-form-label">Expiration Date</label>
        <div class="col-sm-10">
          <datetime
            id="expirationdate"
            v-model="model.expirationDate"
            type="datetime"
            :min-datetime="minDatetime"
            input-class="form-control"
            placeholder="Expiration Date"
          ></datetime>
        </div>
      </div>
      <div class="form-group row">
        <label for="siteAddress" class="col-sm-2 col-form-label">Site Address</label>
        <div class="col-sm-10">
          <input
            type="text"
            v-model="model.siteAddress"
            class="form-control"
            id="siteAddress"
            placeholder="Site Address (www.google.com)"
          >
        </div>
      </div>

      <div class="form-group row">
        <label for="accessCode" class="col-sm-2 col-form-label">Access Code</label>
        <div class="input-group col-sm-10">
          <input
            type="text"
            id="accessCode"
            v-model="model.accessCode"
            class="form-control"
            placeholder="Recipient's username"
            aria-label="Recipient's username"
            aria-describedby="basic-addon2"
          >
          <div class="input-group-append">
            <button class="btn btn-outline-secondary" @click="getNewHash" type="button">Refresh</button>
          </div>
        </div>
      </div>

      <fieldset class="form-group">
        <div class="row">
          <legend class="col-form-label col-sm-2 pt-0">Status</legend>
          <div class="col-sm-10">
            <div class="form-check">
              <input
                class="form-check-input"
                v-model="model.status"
                type="radio"
                name="status"
                id="status-active"
                value="1"
              >
              <label class="form-check-label" for="status-active">Active</label>
            </div>
            <div class="form-check">
              <input
                class="form-check-input"
                v-model="model.status"
                type="radio"
                name="status"
                id="status-deacrive"
                value="0"
              >
              <label class="form-check-label" for="status-deacrive">Deactive</label>
            </div>
          </div>
        </div>
      </fieldset>

      <div class="form-group row">
        <div class="col-sm-10">
          <button type="button" @click="saveChanges" class="btn btn-primary">Save</button>
        </div>
      </div>
    </form>

    <router-link class="btn btn-primary btn-sm" :to="{name:'sites'}">Show Site List</router-link>
  </div>
</template>

<script>
export default {
  name: "editsite",
  data() {
    return {
      model: {
        id: null,
        name: null,
        description: null,
        siteAddress: null,
        expirationDate: null,
        accessCode: null,
        status: null
      },
      minDatetime: new Date().toISOString()
    };
  },
  mounted() {
    if (this.$route.params.id) {
      this.$http
        .get("/api/v1/Site/Get/" + this.$route.params.id)
        .then(response => {
          if (response.data.success) {
            this.model.name = response.data.data.name;
            this.model.description = response.data.data.description;
            this.model.siteAddress = response.data.data.siteAddress;
            this.model.expirationDate = response.data.data.accessCodeEndDate;
            this.model.accessCode = response.data.data.accessCode;
            this.model.status = response.data.data.status;
            this.model.id = response.data.data.id;
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
  },
  methods: {
    saveChanges() {
      this.$http
        .post("/api/v1/Site/Save", this.model)
        .then(response => {
          if (response.data.success) {
            this.$notify({
              type: "success",
              title: "Success",
              text: response.data.message
            });

            this.$router.push({
              name: "sites"
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
    },
    getNewHash(){
      this.$http
        .get("/api/v1/Site/NewHash")
        .then(response => {
          if (response.data.success) {
            this.model.accessCode = response.data.data.accessCode
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
