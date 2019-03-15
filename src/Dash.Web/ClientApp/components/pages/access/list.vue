<template>
  <div>
    <h1>Access Definitions</h1>

    <form>
      <div class="form-group row">
        <label for="fromSite" class="col-sm-12 col-form-label">From Site</label>
        <div class="col-sm-12">
          <model-select
            id="fromSite"
            :options="siteList"
            v-model="fromSite"
            placeholder="Select from site"
          ></model-select>
        </div>
      </div>

      <div class="form-group row">
        <label for="fromSite" class="col-sm-12 col-form-label">To Site</label>
        <div class="col-sm-12">
          <model-select
            id="toSite"
            :options="siteList"
            v-model="toSite"
            placeholder="Select to site"
          ></model-select>
        </div>
      </div>

      <div class="form-group row">
        <div class="col-sm-10">
          <button type="button" @click="addAccessibleSite" class="btn btn-primary">Save</button>
        </div>
      </div>
    </form>

    <table class="table table-striped">
      <thead class="bg-dark text-white">
        <tr>
          <th>#</th>
          <th>From Site</th>
          <th>From Site Address</th>
          <th>To Site</th>
          <th>To Site Address</th>
          <th>Created On</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(access,index) in accessList" :key="'access-'+index">
          <td>{{index + 1}}</td>
          <td>{{access.siteName}}</td>
          <td>{{access.siteAddress}}</td>
          <td>{{access.accessibleSiteName}}</td>
          <td>{{access.accessibleSiteAddress}}</td>
          <td>{{access.createdOn | formatDate}}</td>
          <td>
            <button class="btn btn-danger btn-sm">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import { ModelSelect } from "vue-search-select";

export default {
  name: "accesslist",
  components: {
    "model-select": ModelSelect
  },
  data() {
    return {
      accessList: [],
      siteList: [],
      fromSite: null,
      toSite: null
    };
  },
  mounted() {
    this.getSites();
    this.getAccessList();
  },
  methods: {
    getAccessList() {
      this.$http
        .get("/api/v1/Access/AccessList")
        .then(response => {
          this.accessList = response.data.data;
        })
        .catch(function(err) {
          console.error(err);
        });
    },
    getSites() {
      this.$http
        .get("/api/v1/Access/GetSites")
        .then(response => {
          response.data.data.forEach(element => {
            this.siteList.push({
              value: element.id,
              text: element.name + " => " + element.siteAddress
            });
          });
        })
        .catch(function(err) {
          console.error(err);
        });
    },
    addAccessibleSite(){}
  }
};
</script>


<style>
</style>
