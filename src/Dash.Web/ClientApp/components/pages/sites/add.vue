<template>
  <div>
    <h1>Add New Site</h1>

    <form>
      <div class="form-group row">
        <label for="name" class="col-sm-2 col-form-label">Name</label>
        <div class="col-sm-10">
          <input type="text" class="form-control" id="name" placeholder="Name">
        </div>
      </div>
      <div class="form-group row">
        <label for="description" class="col-sm-2 col-form-label">Description</label>
        <div class="col-sm-10">
          <textarea class="form-control" id="description" placeholder="Description"></textarea>
        </div>
      </div>
      <div class="form-group row">
        <label for="siteAddress" class="col-sm-2 col-form-label">Site Address</label>
        <div class="col-sm-10">
          <input
            type="text"
            class="form-control"
            id="siteAddress"
            placeholder="Site Address (www.google.com)"
          >
        </div>
      </div>

      <fieldset class="form-group">
        <div class="row">
          <legend class="col-form-label col-sm-2 pt-0">Status</legend>
          <div class="col-sm-10">
            <div class="form-check">
              <input
                class="form-check-input"
                type="radio"
                name="gridRadios"
                id="gridRadios1"
                value="option1"
                checked
              >
              <label class="form-check-label" for="gridRadios1">Active</label>
            </div>
            <div class="form-check">
              <input
                class="form-check-input"
                type="radio"
                name="gridRadios"
                id="gridRadios2"
                value="option2"
              >
              <label class="form-check-label" for="gridRadios2">Deactive</label>
            </div>
          </div>
        </div>
      </fieldset>

      <div class="form-group row">
        <div class="col-sm-10">
          <button type="submit" class="btn btn-primary">Save</button>
        </div>
      </div>
    </form>

    <router-link class="btn btn-primary btn-sm" :to="{name:'sites'}">Show Site List</router-link>
  </div>
</template>

<script>
export default {
  name: "addsite",
  data() {
    return {};
  },
  methods: {}
};
</script>

<style>
</style>
