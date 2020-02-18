﻿using NUnit.Framework;
        [Test()]
            //given
            IAopClient client = new DefaultAopClient(TestAccount.Sandbox.Gateway, TestAccount.Sandbox.AppId,
            //when
            AlipayTradeCreateResponse response = client.Execute(request);
            //then
            Assert.AreEqual(response.Code, "10000");

        //TODO:待相关依赖上线后替换为线上环境测试
        public void should_return_success_when_use_sm2()
        {
            //given
            IAopClient client = new DefaultAopClient(TestAccount.DevSM2.Gateway, TestAccount.DevSM2.AppId,
                TestAccount.DevSM2.AppPrivateKey, "json", "1.0", "SM2",
                TestAccount.DevSM2.AlipayPublicKey, "utf-8", false);
            AlipayOpenOperationOpenbizmockBizQueryRequest request = GetRequest();
            //when
            AlipayOpenOperationOpenbizmockBizQueryResponse response = client.Execute(request);
            //then
            Assert.AreEqual(response.Code, "10000");
        }

        private static AlipayOpenOperationOpenbizmockBizQueryRequest GetRequest()
        {
            AlipayOpenOperationOpenbizmockBizQueryRequest request = new AlipayOpenOperationOpenbizmockBizQueryRequest();
            AlipayOpenOperationOpenbizmockBizQueryModel model = new AlipayOpenOperationOpenbizmockBizQueryModel
            {
                BizNo = "test"
            };
            request.SetBizModel(model);
            return request;
        }

        [Test()]
        public void should_be_able_to_send_token()
        {
            IAopClient client = new DefaultAopClient(TestAccount.Sandbox.Gateway, TestAccount.Sandbox.AppId,
            AlipayTradeCreateResponse response = client.Execute(getTradeCreateRequest(), "123", "456");
            Assert.AreEqual(response.SubMsg.Contains("无效的应用授权令牌"), true);
        }

        //TODO:待相关依赖上线后替换为线上环境测试
        public void should_be_able_to_send_target_app_id()
        {
            IAopClient client = new DefaultAopClient(TestAccount.DevSpi.Gateway, TestAccount.DevSpi.AppId,
                  TestAccount.DevSpi.AppPrivateKey, "json", "1.0", "RSA2", TestAccount.DevSpi.AlipayPublicKey, "utf-8");

            AlipaySecurityLcTestQueryRequest request = new AlipaySecurityLcTestQueryRequest();
            request.BizContent = "{\"shop_id\":\"1001\",\"user_id\":\"2088X\",\"status\":\"0\"}";
            AlipaySecurityLcTestQueryResponse response = client.Execute(request, null, null, "2018120560475357");
            Assert.AreEqual(response.Code, "20000");
        }

        [Test()]
            //given
            IAopClient client = new DefaultAopClient(TestAccount.Sandbox.Gateway, TestAccount.Sandbox.AppId,
            //when
            AlipayTradeCreateResponse response = client.Execute(request);
            //then
            Assert.AreEqual(response.Code, "10000");
            //given
            IAopClient client = new DefaultAopClient(TestAccount.Sandbox.Gateway, TestAccount.Sandbox.AppId,
               TestAccount.Sandbox.AppPrivateKey, "json", "1.0", "RSA2", TestAccount.Sandbox.AlipayPublicKey,
               "utf-8", false);
            //when
            AlipayTradeQueryResponse response = client.Execute(request);
            //then
            Assert.AreEqual(response.Body, null);
            //如果Response类中也有同名的Body字段，那么只能用如下方式提取到基类中的Body
            Assert.AreNotEqual(((AopResponse)response).Body, null);

        [Test()]
        public void should_be_able_to_parse_xml_format_response()
        {
            //given
            IAopClient client = new DefaultAopClient(TestAccount.Sandbox.Gateway, TestAccount.Sandbox.AppId,
               TestAccount.Sandbox.AppPrivateKey, "xml", "1.0", "RSA2", TestAccount.Sandbox.AlipayPublicKey,
               "utf-8", false);
            AlipayTradeCreateRequest request = getTradeCreateRequest();
            //when
            AlipayTradeCreateResponse response = client.Execute(request);
            //then
            Assert.AreEqual(response.IsError, false);
            Assert.AreEqual(response.Code, "10000");
        }

        [Test()]
        public void should_be_able_to_parse_xml_format_response_when_encrypted()
        {
            //given
            IAopClient client = new DefaultAopClient(TestAccount.Sandbox.Gateway, TestAccount.Sandbox.AppId,
                TestAccount.Sandbox.AppPrivateKey, "xml", "1.0", "RSA2", TestAccount.Sandbox.AlipayPublicKey,
                "utf-8", TestAccount.Sandbox.AesKey);
            AlipayTradeCreateRequest request = getTradeCreateRequest();
            request.SetNeedEncrypt(true);
            //when
            AlipayTradeCreateResponse response = client.Execute(request);
            //then
            Assert.AreEqual(response.IsError, false);
            Assert.AreEqual(response.Code, "10000");
        }

        [Test()]
        public void should_return_false_when_app_not_set_private_key()
        {
            //given
            //访问线上一个没有设置公私钥对的APP
            IAopClient client = new DefaultAopClient(TestAccount.ProdCert.Gateway, TestAccount.NotSetKeyAppId,
                    TestAccount.Sandbox.AppPrivateKey, "json", "1.0", "RSA2", TestAccount.Sandbox.AlipayPublicKey, "utf-8", false);
            //when
            AlipayTradeCreateResponse response = client.Execute(getTradeCreateRequest());
            //then
            Assert.AreEqual(response.IsError, true);
            Assert.AreEqual(response.SubMsg.Contains("应用未配置对应签名算法的公钥或者证书"), true);
        }

        [Test]
        public void should_get_exception_when_call_cert_execute()
        {
            //given
            IAopClient client = new DefaultAopClient(TestAccount.Sandbox.Gateway, TestAccount.Sandbox.AppId,

            //then
            AopException ex = Assert.Throws<AopException>(() => client.CertificateExecute(request));
            Assert.AreEqual(ex.Message.Contains("检测到证书相关参数未初始化，非证书模式下请改为调用Execute"), true);
        }

        private static AlipayTradeCreateRequest getTradeCreateRequest()
        {
            AlipayTradeCreateRequest request = new AlipayTradeCreateRequest();
            AlipayTradeCreateModel model = new AlipayTradeCreateModel();
            return request;
        }
    }