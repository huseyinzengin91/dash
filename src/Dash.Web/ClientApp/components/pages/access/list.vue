<template>
  <div>
    <content-placeholders v-if="!isLoaded">
      <content-placeholders-heading :img="true"/>
      <content-placeholders-text :lines="3"/>
    </content-placeholders>

    <div v-if="isLoaded">
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
            <button
              type="button"
              @click="addAccessibleSite"
              :class="{ disabled: (!toSite || !fromSite)}"
              class="btn btn-primary"
            >Save</button>
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
              <button class="btn btn-danger btn-sm" type="button" @click="deleteAccess(access.id)" >Delete</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
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
      isLoaded: false,
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
          this.isLoaded = true;
        })
        .catch(function(err) {
          console.error(err);
        });
    },
    addAccessibleSite() {     
      this.$http
        .post("/api/v1/Access/Add", {
          SiteId: this.fromSite,
          AccessibleSiteId: this.toSite
        })
        .then(response => {
          if (
            response.data.success &&
            response.data.data &&
            response.data.data.id
          ) {
            this.$notify({
              type: "success",
              title: "Success",
              text: response.data.message
            });
            this.toSite = null;
            this.fromSite = null;
            this.getAccessList();
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
    deleteAccess(id) {
      debugger
      this.$http
        .post("/api/v1/Access/Delete", {
          Id: id
        })
        .then(response => {
          if (response.data.success) {
            this.$notify({
              type: "success",
              title: "Success",
              text: response.data.message
            });
            this.getAccessList();
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
