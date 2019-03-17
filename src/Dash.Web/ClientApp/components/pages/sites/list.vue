<template>
  <div>
    <content-placeholders v-if="!isLoaded">
      <content-placeholders-heading :img="true"/>
      <content-placeholders-text :lines="3"/>
    </content-placeholders>

    <div v-if="isLoaded">
      <h1>List of Sites</h1>
      <router-link class="btn btn-primary btn-sm" :to="{name:'add-site'}">Add New Site</router-link>
      <br>
      <br>

      <table class="table table-striped">
        <thead class="bg-dark text-white">
          <tr>
            <th>#</th>
            <th>Name</th>
            <th>Site Address</th>
            <th>Status</th>
            <th>CreatedOn</th>
            <th>ModifiedOn</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(site,index) in siteList" :key="'site-'+index">
            <td>{{index + 1}}</td>
            <td>{{site.name}}</td>
            <td>{{site.siteAddress}}</td>
            <td>
              <span v-if="site.status === 1" class="badge badge-success">{{site.statusText}}</span>
              <span v-if="site.status === 0" class="badge badge-danger">{{site.statusText}}</span>
            </td>
            <td>{{site.createdOn | formatDate}}</td>
            <td>{{site.modifiedOn | formatDate}}</td>
            <td>
              <router-link
                class="btn btn-primary btn-sm"
                tag="button"
                :to="{name:'edit-site', params: {id:site.id}}"
              >Edit</router-link>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
export default {
  name: "sitelist",
  data() {
    return {
      isLoaded: false,
      siteList: []
    };
  },
  created: function() {
    this.getSites();
  },
  methods: {
    getSites() {
      this.$http
        .get("/api/v1/Site/GetSites")
        .then(response => {
          this.siteList = response.data.data;
          this.isLoaded = true;
        })
        .catch(function(err) {
          console.error(err);
        });
    }
  }
};
</script>

<style>
</style>
